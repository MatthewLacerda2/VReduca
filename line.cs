using UnityEngine;

public class line {

    public Vector3 origin;
    public Vector3 direction;

    public line normalized {
        get {
            return new line(origin, direction.normalized);
        }
    }

    public line centralized {
        get {
            return new line(new Vector3(0, 0, 0), direction - origin);
        }
    }

    public float length {
        get {
            return Vector3.Distance(direction, origin);
        }
    }

    public line() {
        origin = new Vector3(0, 0, 0);
        direction = new Vector3(0, 0, 0);
    }

    public line(Vector3 o, Vector3 d) {
        origin = o;
        direction = d;
    }

    public void normalize() {
        direction.Normalize();
    }

    public void centralizar() {
        direction -= origin;
        origin = new Vector3(0, 0, 0);
    }
}