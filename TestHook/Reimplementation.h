#pragma once

// Abe Raycasting. Currently has some clipping issues.
bool Abe_Raycast(AE_PathList * collisionData, signed int _line1p1x, signed int _line1p1y, signed int _line1p2x, signed int _line1p2y, AE_pathLine ** collision, _DWORD *collisionX, _DWORD *collisionY, int mode)
{
	AE_pathLine * nearestLine = nullptr;
	float nearestCollisionX = 0;
	float nearestCollisionY = 0;
	float nearestDistance = 0.f;
	bool hasCollided = false;
	bool firstCollision = true;

	if (!collisionData->Count)
	{
		*collision = nullptr;
		return false;
	}

	for (int i = 0; i < collisionData->Count; i++)
	{
		AE_pathLine * currentLine = &collisionData->Data[i];

		// Game specific
		if (!(((1 << currentLine->Mode) % 32) & mode)) // if (!(currentLine->Mode & mode))
			continue;

		bool segments_intersect = false;
		float intersectionX = 0;
		float intersectionY = 0;

		// Converting to fixed point only needed for real game.
		// Alive won't need this
		int line1p1x = _line1p1x / 0x10000;
		int line1p1y = _line1p1y / 0x10000;
		int line1p2x = _line1p2x / 0x10000;
		int line1p2y = _line1p2y / 0x10000;

		int line2p1x = currentLine->X1;
		int line2p1y = currentLine->Y1;
		int line2p2x = currentLine->X2;
		int line2p2y = currentLine->Y2;

		// Get the segments' parameters.
		int dx12 = line1p2x - line1p1x;
		int dy12 = line1p2y - line1p1y;
		int dx34 = line2p2x - line2p1x;
		int dy34 = line2p2y - line2p1y;

		// Solve for t1 and t2
		float denominator = (dy12 * dx34 - dx12 * dy34);
		float t1 = ((line1p1x - line2p1x) * dy34 + (line2p1y - line1p1y) * dx34) / denominator;

		if (isinf(t1))
			continue;

		float t2 = ((line2p1x - line1p1x) * dy12 + (line1p1y - line2p1y) * dx12) / -denominator;

		// Find the point of intersection.
		intersectionX = line1p1x + dx12 * t1;
		intersectionY = line1p1y + dy12 * t1;

		// The segments intersect if t1 and t2 are between 0 and 1.
		hasCollided = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

		if (hasCollided)
		{
			float distance = sqrtf(powf((line1p1x - intersectionX), 2) + powf((line1p1y - intersectionY), 2));

			if (firstCollision || distance < nearestDistance)
			{
				nearestCollisionX = intersectionX;
				nearestCollisionY = intersectionY;
				nearestDistance = distance;
				nearestLine = currentLine;

				firstCollision = false;
			}
		}
	}

	if (nearestLine)
	{
		*collisionX = (int)(nearestCollisionX * 0x10000);
		*collisionY = (int)(nearestCollisionY * 0x10000);
		*collision = nearestLine;
		return true;
	}

	*collision = nullptr;
	return false;
}