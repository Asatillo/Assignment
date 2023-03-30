using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessboard
{
    public class ChessboardMatrix
    {
        #region Exceptions
        public class NegativeSizeException : Exception { };
        public class ReferenceToNullPartException : Exception { };
        public class DifferentSizeException : Exception { };
        #endregion

        #region Attribute
        private List<double> x = new ();
        private readonly int size;
        private readonly int length;
        #endregion

        #region Constructors

        public ChessboardMatrix(int k)
        {   
            size = k;
            if (k <= 0) throw new NegativeSizeException();
            for(int i=0; i<k; i++){
                for(int j=0; j<k; j++){
                    if (IsColored(i, j)){
                        x.Add(0);
                    }  
                }
            }
            length = x.Count;
        }

        public ChessboardMatrix(ChessboardMatrix d){
            size = d.Size;
            length = d.Length;
            for (int i = 0; i < d.x.Count; ++i)
            {
                x.Add(d.x[i]);
            }
        }

        #endregion

        #region Properties

        // Property for getting the size of the matrix
        public int Size 
        {
            get { return size; }
        }

        // Property for getting the length of the List where matrix is saved
        public int Length
        {
            get { return length; }
        }

        // Property for getting and setting an element with square bracket
        public double this[int i, int j] 
        {
            get
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if (IsColored(i, j)) return x[(i*Size+j)/2];
                else return 0;
            }
            set
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if (IsColored(i, j)) x[(i*Size+j)/2] = value;
                else throw new ReferenceToNullPartException();
            }
        }

        #endregion

        #region Getters and setters
        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2);
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    if(IsColored(i, j)){
                        str += "\t" + this[i, j];    
                    }else{
                        str += "\tx";
                    }
                }
                str += "\n";
            }
            return str;
        }

        public void Set(in List<double> x)
        {
            if (this.Length != x.Count) throw new DifferentSizeException();
            for (int i = 0; i < length; i++)
            {
                this.x[i] = x[i];
            }
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null || obj is not ChessboardMatrix)
                return false;
            else
            {
                ChessboardMatrix? chessboard = obj as ChessboardMatrix;
                if (chessboard!.Size != this.Size) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i] != chessboard.x[i]) return false;
                }
                return true;
            }
        }
        #endregion

        #region Operators

        public static ChessboardMatrix operator +(ChessboardMatrix a, ChessboardMatrix b){
            if (a.Size != b.Size) throw new DifferentSizeException();
            ChessboardMatrix c = new(a.Size);
            for (int i = 0; i < c.Length; ++i){
                c.x[i] = a.x[i] + b.x[i];
            }
            return c;
        }

        public static ChessboardMatrix operator *(ChessboardMatrix a, ChessboardMatrix b){
            if (a.Size != b.Size) throw new DifferentSizeException();
            ChessboardMatrix c = new(a.Size);
            for (int i = 0; i < c.Length; ++i){
                c.x[i] = a.x[i] * b.x[i];
            }
            return c;
        }
        #endregion

        #region Helpers

        public static bool IsColored(int i, int j) => (i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0);

        #endregion
    }
}
