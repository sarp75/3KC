using System.Text;

namespace _2ds {
    public class Vector3
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    // Constructor 4 vector
    public Vector3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Add two vectors
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    // Subtract two vectors
    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    // Multiply vector by a numvber
    public static Vector3 operator *(Vector3 a, int num)
    {
        return new Vector3(a.X * num, a.Y * num, a.Z * num);
    }

    // Calculate the dot product of two vectors
    public static int Dot(Vector3 a, Vector3 b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }

    // Cross product
    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X
        );
    }

    // Thanks mister Pythagoras!!!
    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    // Divide edges of vector by length of the vector to get a vector with the length of 1
    public Vector3 Normalize()
    {
        double length = Length();
        return new Vector3((int)(X / length), (int)(Y / length), (int)(Z / length));
    }

    // Override ToString method for easy display
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}

class Dismensional {

    //this is the replication of a really common but effective way of encryption    
    public void ArrayCommon(char[] message, char[] key) {

        if(message.Length < 16 || key.Length < 16)
			{
				Console.WriteLine("short message or key, retry!!!");
				return;
			}
			char[] encrypted =new char[16] ;
        for(int i = 0; i < 4; i++) {//x
            for(int j = 0; j < 4; j++) {//y
                encrypted[i+4*j] = (char)((int)message[i + 4 * j] ^ (int)key[i + 4 * j]); //take the xor of message(x,y) and key(x,y)
				Console.WriteLine($"Encrypted char (Letter):" + (int)message[i+4*j]);
				}
        }
    }
}

}