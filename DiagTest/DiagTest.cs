using ChessboardMatrix;

namespace ChessboardMatrixTest
{
    [TestClass]
    public class ChessboardMatrixTest
    {
        [TestMethod]
        public void Create()
        {
            Assert.ThrowsException<ChessboardMatrix.NegativeSizeException>(() => _ = new ChessboardMatrix(0));
            ChessboardMatrix a = new(1);
            Assert.AreEqual(a[0, 0], 0);
            Assert.AreEqual(a.Size, 1);

            ChessboardMatrix b = new(2);
            Assert.AreEqual(b.Size, 2);

            ChessboardMatrix c = new(5);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(c[i, j], 0);
                }
            }
            Assert.AreEqual(c.Size, 5);
            Assert.ThrowsException<ChessboardMatrix.NegativeSizeException>(() => _ = new ChessboardMatrix(-1));
            ChessboardMatrix d = new ChessboardMatrix(1000);
            Assert.AreEqual(d.Size, 1000);
        }

        [TestMethod]
        public void Change()
        {
            ChessboardMatrix c = new(3);
            c[0, 0] = 1;
            c[1, 1] = 1;
            c[2, 2] = 1;

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(c[i, i], 1);
            }

            Assert.AreEqual(c[0, 1], 0);
            Assert.ThrowsException<ChessboardMatrix.ReferenceToNullPartException>(() => c[1, 0] = 3);
        }

        [TestMethod]
        public void Assignment()
        {
            ChessboardMatrix a = new ChessboardMatrix(3);
            a[0, 0] = 1;
            a[1, 1] = 2;
            a[2, 2] = 3;
            ChessboardMatrix b = new ChessboardMatrix(a);
            ChessboardMatrix c = new ChessboardMatrix(2);
            c = a;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals(c));
            b[0, 0] = 0;
            Assert.IsFalse(a.Equals(b));
            c[0, 0] = 0;
            Assert.IsTrue(a.Equals(b));
            a = b = c;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals(c));
            a = a;
            Assert.IsTrue(a.Equals(a));
        }


        [TestMethod]
        public void Add()
        {
            ChessboardMatrix a = new(3);
            ChessboardMatrix b = new(3);
            ChessboardMatrix zero = new(3);
            ChessboardMatrix d = new(2);
            ChessboardMatrix c;

            a[0, 0] = 1;
            a[1, 1] = 1;
            a[2, 2] = 1;

            b[0, 0] = 42;
            b[1, 1] = 0;
            b[2, 2] = 0;

            c = a + b;

            Assert.AreEqual(c[0, 0], 43);
            Assert.AreEqual(c[1, 1], 1);
            Assert.IsTrue(a.Equals(a + zero));
            Assert.IsTrue(a.Equals(zero + a));
            Assert.IsTrue((a + b).Equals(b + a));
            Assert.IsTrue(((a + b)+c).Equals(a + (b+c)));

            Assert.ThrowsException<ChessboardMatrix.DifferentSizeException>(() => a + d);
        }

        [TestMethod]
        public void Mul()
        {
            ChessboardMatrix a = new(3);
            ChessboardMatrix b = new(3);
            ChessboardMatrix d = new(2);
            ChessboardMatrix zero = new(3);
            ChessboardMatrix c;

            a[0, 0] = 1;
            a[1, 1] = 1;
            a[2, 2] = 1;

            b[0, 0] = 42;
            b[1, 1] = 0;
            b[2, 2] = 0;

            c = a * b;

            Assert.AreEqual(c[0, 0], 42);
            Assert.AreEqual(c[1, 1], 0);

            Assert.IsTrue(zero.Equals(a * zero));
            Assert.IsTrue(b.Equals(a * b));
            Assert.IsTrue((a * (b*c)).Equals((a * b)*c));
            Assert.IsTrue((b * c).Equals(c * b));

            Assert.ThrowsException<ChessboardMatrix.DifferentSizeException>(() => a * d);
        }

        [TestMethod]
        public void SetMatrix()
        {
            List<double> vec = new List<double>() { 1, 2, 3 };
            ChessboardMatrix a = new ChessboardMatrix(3);
            ChessboardMatrix b = new ChessboardMatrix(2);

            Assert.AreEqual(a[0, 0], 0);
            Assert.AreEqual(a[1, 1], 0);
            Assert.AreEqual(a[2, 2], 0);
            a.Set(vec);
            Assert.AreEqual(a[0, 0], 1);
            Assert.AreEqual(a[1, 1], 2);
            Assert.AreEqual(a[2, 2], 3);

            Assert.ThrowsException<ChessboardMatrix.DifferentSizeException>(() => b.Set(vec));
        }
    }
}