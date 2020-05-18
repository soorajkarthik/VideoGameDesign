﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScribblePlatformer
{
    public static class RectangleExtensions
    {
        public static Vector2 GetIntersectionDepth(this Rectangle _rectA, Rectangle _rectB)
        {
            float halfWidthA = _rectA.Width / 2.0f;
            float halfHeightA = _rectA.Height / 2.0f;

            float halfWidthB = _rectB.Width / 2.0f;
            float halfHeightB = _rectB.Height / 2.0f;

            Vector2 centerA = new Vector2(_rectA.Left + halfWidthA, _rectA.Top + halfHeightA);
            Vector2 centerB = new Vector2(_rectB.Left + halfWidthB, _rectB.Top + halfHeightB);

            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;

            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;

            return new Vector2(depthX, depthY);
        }

    }
}
