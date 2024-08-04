using UnityEngine;

public static class Utils {
    public static bool ContainsLayer(LayerMask mask, int layer) {
        return mask == (mask | (1 << layer));
    }

    public static float Rem(float x, float m) {
        float r = x % m;
        return r<0 ? r + m : r;
    }
}