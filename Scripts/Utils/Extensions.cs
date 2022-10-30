using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public static class Extentions
{
    public static void SetTempVariable<T>(this Node n, string variable, float seconds, T start, T end = default(T))
    {
        n.Set(variable, start);
        n.GetTree().CreateTimer(seconds,false).Connect("timeout", n, "set", new Godot.Collections.Array{variable, end});
    }

    public static void SetDeferredTempVariable<T>(this Node n, string variable, float seconds, T start, T end = default(T))
    {
        n.SetDeferred(variable, start);
        n.GetTree().CreateTimer(seconds,false).Connect("timeout", n, "set_deferred", new Godot.Collections.Array{variable, end}, (uint)Godot.Object.ConnectFlags.Deferred);
    }

    public static Vector2 Center(this Rect2 rec) => rec.Position + rec.Size/2f;
		
	public static Rect2 Clamp(this Rect2 rec, float mx, float Mx, float my, float My)
	{
		var rpos = new Vector2(mx,my);
		var rend = new Vector2(Mx,My);
		var rsize = (rend-rpos).Abs();
		var r = new Rect2(rpos,rsize);
		return rec.Clip(r);
	}
    
    public static void DrawShape(this CanvasItem ci, Shape2D shape, Color color) => shape.Draw(ci.GetCanvasItem(), color);

    public static Rect2 RectWithAll(this IEnumerable<Vector2> ev)
	{
		var agg = ev.Aggregate<Vector2, (Vector2,Vector2)>((Vector2.Inf,-Vector2.Inf), (a,v) => (a.Item1.Min(v), a.Item2.Max(v)));
		var pos = agg.Item1;
		var end = agg.Item2;
		var size = (end-pos).Abs();
		return new Rect2(pos,size);
	}

    public static Vector2 Max(this Vector2 v1, Vector2 v2) => new Vector2(Math.Max(v1.x,v2.x), Math.Max(v1.y,v2.y));
    public static Vector2 Min(this Vector2 v1, Vector2 v2) => new Vector2(Math.Min(v1.x,v2.x), Math.Min(v1.y,v2.y));

    public static bool EqualsIgnoreCase(this string s1, string s2) => string.Equals(s1, s2,  StringComparison.OrdinalIgnoreCase);
}