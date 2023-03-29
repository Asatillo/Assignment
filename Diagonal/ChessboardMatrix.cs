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
        private List<int> x = new ();
        private int size;
        #endregion

        #region Constructors

        public ChessboardMatrix(int k)
        {   
            size = k;
            if (k <= 0) throw new NegativeSizeException();
            for(int i=0; i<k; i++){
                for(int j=0; j<k; j++){
                    if((i%2 == 0 && j%2==0) || (i%2 != 0 && j%2!=0)){
                        x.add(0);
                    }
                }
            }
        }

        public ChessboardMatrix(ChessboardMatrix d){
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

        // Property for getting and setting an element with square bracket
        public double this[int i, int j] 
        {
            get
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if ((i%2 == 0 && j%2==0) || (i%2 != 0 && j%2!=0)) return x[((i-1)*Size+(j-1))/2];
                else return 0;
            }
            set
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if ((i%2 == 0 && j%2==0) || (i%2 != 0 && j%2!=0)) x[((i-1)*Size+(j-1))/2] = value;
                else throw new ReferenceToNullPartException();
            }
        }

        #endregion

        #region Getters and setters

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    if((i%2 == 0 && j%2==0) || (i%2 != 0 && j%2!=0)){
                        str += "\t" + this[i, j];    
                    }else{
                        str += "\tx";
                    }
                }
                str += "\n";
            }
            return str;
        }

        public int Get(int i, int j){
            if(i > size || j > size) throw new DifferentSizeException();
            if((i%2 != 0 && j%2!=0) || (i%2 == 0 && j%2==0)) return 0;
            return x[((i-1)*Size+(j-1))/2];
        }

        #endregion

        #region Operators

        public static ChessboardMatrix operator +(ChessboardMatrix a, ChessboardMatrix b){
            if (a.Size != b.Size) throw new DifferentSizeException();
            ChessboardMatrix c = new(a.Size);
            for (int i = 0; i < c.Size; ++i){
                c.x[i] = a.x[i] + b.x[i];
            }
            return c;
        }

        public static ChessboardMatrix operator *(ChessboardMatrix a, ChessboardMatrix b){
            if (a.Size != b.Size) throw new DifferentSizeException();
            ChessboardMatrix c = new(a.Size);
            for (int i = 0; i < c.Size; ++i){
                c.x[i] = a.x[i] * b.x[i];
            }
            return c;
        }
        #endregion
    }
}
