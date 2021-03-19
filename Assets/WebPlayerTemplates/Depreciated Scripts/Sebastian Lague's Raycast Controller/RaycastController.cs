using UnityEngine;

public class RaycastController : MonoBehaviour
{
	[SerializeField]
	protected LayerMask collisionMask;

	protected const float SKIN_WIDTH = .015f;
	const float DST_BETWEEN_RAYS = .25f;

	protected int horizontalRayCount;
	protected int verticalRayCount;

	protected float horizontalRaySpacing;
	protected float verticalRaySpacing;

	protected BoxCollider2D coll;
	protected RaycastOrigins raycastOrigins;

	public virtual void Awake()
	{
		coll = GetComponent<BoxCollider2D>();
	}

	public virtual void Start()
	{
		CalculateRaySpacing();
	}

	public void UpdateRaycastOrigins()
	{
		Bounds bounds = coll.bounds;
		bounds.Expand(SKIN_WIDTH * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	public void CalculateRaySpacing()
	{
		Bounds bounds = coll.bounds;
		bounds.Expand(SKIN_WIDTH * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;

		horizontalRayCount = Mathf.RoundToInt(boundsHeight / DST_BETWEEN_RAYS);
		verticalRayCount = Mathf.RoundToInt(boundsWidth / DST_BETWEEN_RAYS);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	public struct RaycastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}