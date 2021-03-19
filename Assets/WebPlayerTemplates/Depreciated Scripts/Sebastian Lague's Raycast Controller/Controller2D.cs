using UnityEngine;
public class Controller2D : RaycastController
{
	private float maxSlopeAngle = 80f;

	public CollisionInfo collisions;

	public override void Start()
	{
		base.Start();
	}

	/*
	public void Move(Vector2 moveAmount)
	{
		Move(moveAmount, 1);
	} */

	public void Move(Vector2 moveAmount, int faceDir)
	{
		UpdateRaycastOrigins();

		collisions.Reset();
		collisions.moveAmountOld = moveAmount;

		if (moveAmount.y < 0)
		{
			DescendSlope(ref moveAmount);
		}

		HorizontalCollisions(ref moveAmount, faceDir);
		if (moveAmount.y != 0)
		{
			VerticalCollisions(ref moveAmount);
		}

		transform.Translate(moveAmount);
	}

	void HorizontalCollisions(ref Vector2 moveAmount, int faceDir)
	{
		float rayLength = Mathf.Abs(moveAmount.x) + SKIN_WIDTH;

		if (Mathf.Abs(moveAmount.x) < SKIN_WIDTH)
		{
			rayLength = 2 * SKIN_WIDTH;
		} 

		for (int i = 0; i < horizontalRayCount; i++)
		{
			Vector2 rayOriginFront = raycastOrigins.bottomRight;
			Vector2 rayOriginBehind = raycastOrigins.bottomLeft;

			if (faceDir == -1)
            {
				rayOriginFront = raycastOrigins.bottomLeft;
				rayOriginBehind = raycastOrigins.bottomRight;
			}

			rayOriginFront += Vector2.up * (horizontalRaySpacing * i);
			rayOriginBehind += Vector2.up * (horizontalRaySpacing * i);

			RaycastHit2D hitFront = Physics2D.Raycast(rayOriginFront, Vector2.right * faceDir, rayLength, collisionMask);
			RaycastHit2D hitBehind = Physics2D.Raycast(rayOriginFront, Vector2.right * -faceDir, rayLength, collisionMask);

			Debug.DrawRay(rayOriginFront, Vector2.right * faceDir, Color.red);
			Debug.DrawRay(rayOriginBehind, Vector2.right * -faceDir, Color.red);

			if (hitFront)
			{

				if (hitFront.distance == 0)
				{
					continue;
				}

				float slopeAngle = Vector2.Angle(hitFront.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxSlopeAngle)
				{
					if (collisions.descendingSlope)
					{
						collisions.descendingSlope = false;
						moveAmount = collisions.moveAmountOld;
					}
					float distanceToSlopeStart = 0;
					if (slopeAngle != collisions.slopeAngleOld)
					{
						distanceToSlopeStart = hitFront.distance - SKIN_WIDTH;
						moveAmount.x -= distanceToSlopeStart * faceDir;
					}
					ClimbSlope(ref moveAmount, slopeAngle, hitFront.normal);
					moveAmount.x += distanceToSlopeStart * faceDir;
				}

				if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle)
				{
					moveAmount.x = (hitFront.distance - SKIN_WIDTH);
					rayLength = hitFront.distance;

					if (collisions.climbingSlope)
					{
						moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
					}

					collisions.infront = true;
					Debug.Log("Infront");
				}
			}

            if (hitBehind)
            {
				collisions.behind = true;
				Debug.Log("Behind");
			}
		}
	}

	void VerticalCollisions(ref Vector2 moveAmount)
	{
		float directionY = Mathf.Sign(moveAmount.y);
		float rayLength = Mathf.Abs(moveAmount.y) + SKIN_WIDTH;

		for (int i = 0; i < verticalRayCount; i++)
		{
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

			if (hit)
			{
				moveAmount.y = (hit.distance - SKIN_WIDTH) * directionY;
				rayLength = hit.distance;

				if (collisions.climbingSlope)
				{
					moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
				}

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}

		if (collisions.climbingSlope)
		{
			float directionX = Mathf.Sign(moveAmount.x);
			rayLength = Mathf.Abs(moveAmount.x) + SKIN_WIDTH;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle)
				{
					moveAmount.x = (hit.distance - SKIN_WIDTH) * directionX;
					collisions.slopeAngle = slopeAngle;
					collisions.slopeNormal = hit.normal;
				}
			}
		}
	}

	void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal)
	{
		float moveDistance = Mathf.Abs(moveAmount.x);
		float climbmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (moveAmount.y <= climbmoveAmountY)
		{
			moveAmount.y = climbmoveAmountY;
			moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
			collisions.slopeNormal = slopeNormal;
		}
	}

	void DescendSlope(ref Vector2 moveAmount)
	{
		RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + SKIN_WIDTH, collisionMask);
		RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + SKIN_WIDTH, collisionMask);
		if (maxSlopeHitLeft ^ maxSlopeHitRight)
		{
			SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
			SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
		}

		if (!collisions.slidingDownMaxSlope)
		{
			float directionX = Mathf.Sign(moveAmount.x);
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

			if (hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle)
				{
					if (Mathf.Sign(hit.normal.x) == directionX)
					{
						if (hit.distance - SKIN_WIDTH <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
						{
							float moveDistance = Mathf.Abs(moveAmount.x);
							float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
							moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
							moveAmount.y -= descendmoveAmountY;

							collisions.slopeAngle = slopeAngle;
							collisions.descendingSlope = true;
							collisions.below = true;
							collisions.slopeNormal = hit.normal;
						}
					}
				}
			}
		}
	}

	void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
	{
		if (hit)
		{
			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle > maxSlopeAngle)
			{
				moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);

				collisions.slopeAngle = slopeAngle;
				collisions.slidingDownMaxSlope = true;
				collisions.slopeNormal = hit.normal;
			}
		}
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool behind, infront;

		public bool climbingSlope;
		public bool descendingSlope;
		public bool slidingDownMaxSlope;

		public float slopeAngle, slopeAngleOld;
		public Vector2 slopeNormal;
		public Vector2 moveAmountOld;

		public void Reset()
		{
			above = below = false;
			infront = behind = false;
			climbingSlope = false;
			descendingSlope = false;
			slidingDownMaxSlope = false;
			slopeNormal = Vector2.zero;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}

}