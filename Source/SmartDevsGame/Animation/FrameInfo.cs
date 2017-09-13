using Microsoft.Xna.Framework;

namespace SmartDevsGame.Animation
{
	public class FrameInfo
	{
		public FrameInfo(bool isEmpty, int rowsEmpty)
		{
			this.IsEmpty = isEmpty;
			this.RowsEmpty = rowsEmpty;
		}

		public bool IsEmpty { get; }
		public int RowsEmpty { get; }
	}
}