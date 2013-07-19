using System;

namespace Pantheon
{
	public class MatrixException: Exception
	{
		public MatrixException()
		{
		}

		public MatrixException(string message)
			: base(message)
		{
		}

		public MatrixException(string message, Exception inner)
			: base(message, inner)
		{
		}

	}

	/// <summary>
	/// Summary description for Matrix.
	/// </summary>
	public class Matrix
	{
		private int m_Columns = 0;
		private int m_Rows = 0;
		private float[,] m_Matrix;
		private int iDF = 1;
		
		/// <summary>
		/// default constructor
		/// </summary>
		private Matrix()
		{
		}

		/// <summary>
		/// Make a new matrix
		/// </summary>
		/// <param name="rows">number of rows</param>
		/// <param name="columns">number of columns</param>
		public Matrix(int rows, int columns)
		{
			m_Matrix = new float[rows, columns];
			m_Columns = columns;
			m_Rows = rows;
		}

		/// <summary>
		/// Get the value of the cell
		/// </summary>
		/// <param name="row">#row</param>
		/// <param name="col">#column</param>
		/// <returns>value</returns>
		private float GetCells(int row, int col)
		{
			if((row >= m_Rows) || (col >= m_Columns))
				throw(new MatrixException("Indexes are out of range"));
			
			return m_Matrix[row, col];
		}

		/// <summary>
		/// Set the value of the cell
		/// </summary>
		/// <param name="row">#row</param>
		/// <param name="col">#column</param>
		/// <param name="val">value</param>
		/// <returns></returns>
		private void SetCells(int row, int col, float val)
		{
			if((row >= m_Rows) || (col >= m_Columns))
				throw(new MatrixException("Indexes are out of range"));
			
			m_Matrix[row, col] = val;
		}

		/// <summary>
		/// swaps two variables
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		private void swap(ref float a, ref float b)
		{
			float temp;
			temp = a;
			a = b;
			b = temp;
		}

		/// <summary>
		/// Gauss Triangularization
		/// </summary>
		/// <returns></returns>
		public Matrix GausTrangularization()
		{
			if(m_Columns != m_Rows)
				throw(new MatrixException("The matrix cannot be triangularized."));

			Matrix mat = this.Clone();
			int v;
			float temp;

			for(int j = 0; j < (m_Rows - 1); j++)
				for(int i = j + 1; i < m_Rows; i++)
				{
					v = 1;
					while(mat[j, j] == 0)
					{
						if(j + v >= m_Rows)
						{
							iDF = 0;
							break;
						}
						else
						{
							for(int c = 0; c < m_Rows; c++)
							{
								temp = mat[j, c];
								mat[j, c] = mat[j + v, c];
								mat[j + v, c] = temp;
							}
							v++;
							iDF *= -1;
						}
					}
					if(mat[j, j] != 0)
					{
						temp = -1 * mat[i, j] / mat[j, j];
						for(int k = j; k < m_Rows; k++)
							mat[i, k] += temp * mat[j, k];
					}
				}

			return mat;
		}

		/// <summary>
		/// Compute the cofactor of a matrix
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public Matrix CoFactor(int a, int b)
		{
			Matrix mat = new Matrix(m_Rows, m_Rows);
			Matrix m = new Matrix(m_Rows - 1, m_Rows - 1);
			int x = 0;
			int i = 0;
			int y = 0;
			int j = 0;
			while(x < m_Rows)
			{
				while(y < m_Rows)
				{
					if((x != a) && (y != b))
						mat[i, j++] = this[x, y];
					y++;
				}
				if((x != a) && (y != b)) i++;
				j = 0;
				y = 0;
				x++;
			}
			for(i = 0; i < m_Rows - 1; i++)
				for(j = 0; j < m_Rows - 1; j++)
					m[i, j] = mat[i, j];
			return m;
		}

		/// <summary>
		/// Adjoint of the matrix
		/// </summary>
		/// <returns></returns>
		public Matrix Adjoint()
		{
			if(m_Columns != m_Rows)
				throw(new MatrixException("The matrix cannot be adjoint."));

			Matrix m = new Matrix(m_Rows, m_Rows);
			float det;

			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Rows; j++)
				{
					det = CoFactor(i, j).Determinant();
					m[i, j] = (float)Math.Pow(-1, i + j) * det;
				}
			return m.Transposed();
		}

		/// <summary>
		/// Gets the lower matrix
		/// </summary>
		/// <returns></returns>
		public Matrix LowerTriangulation()
		{
			Matrix mat;
			
			// the # rows and # columns must identical
			if(m_Rows != m_Columns)
				return null;

			mat = this.Clone();
			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Rows; j++)
					if(j > i)
						mat[i, j] = 0.0F;
			return mat;
		}

		/// <summary>
		/// Gets the upper matrix
		/// </summary>
		/// <returns></returns>
		public Matrix UpperTriangulation()
		{
			Matrix mat;
			
			// the # rows and # columns must identical
			if(m_Rows != m_Columns)
				return null;

			mat = this.Clone();
			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Rows; j++)
					if(j < i)
						mat[i, j] = 0.0F;
			return mat;
		}

		/// <summary>
		/// Update an entire row
		/// </summary>
		/// <param name="row">#row to update</param>
		/// <param name="vals">new values</param>
		public void AssignToRow(int row, float[] vals)
		{
			if(m_Columns != vals.Length)
				return;

			for(int i = 0; i < m_Columns; i++)
				m_Matrix[row, i] = vals[i];
		}
		
		/// <summary>
		/// Update an entire column
		/// </summary>
		/// <param name="col">#column to update</param>
		/// <param name="vals">new values</param>
		public void AssignToColumn(int col, float[] vals)
		{
			if(m_Rows != vals.Length)
				return;

			for(int i = 0; i < m_Columns; i++)
				m_Matrix[i, col] = vals[i];
		}

		/// <summary>
		/// Multiply two matrix
		/// </summary>
		/// <param name="AMat"></param>
		/// <param name="BMat"></param>
		/// <returns></returns>
		public static Matrix operator *(Matrix AMat, Matrix BMat)
		{
			int i, j, k;

			if(AMat.NrColumns != BMat.NrRows)
				throw(new MatrixException("The matrix cannot be multiplied."));
			
			Matrix mat = new Matrix(AMat.NrRows, BMat.NrColumns);
			for(k = 0; k < AMat.NrRows; k++)
				for(i = 0; i < BMat.NrColumns; i++)
					for(j = 0; j < AMat.NrColumns; j++)
						mat[k, i] += AMat[k, j] * BMat[j, i];
			return mat;
		}
		
		/// <summary>
		/// Sum two matrix
		/// </summary>
		/// <param name="AMat"></param>
		/// <param name="BMat"></param>
		/// <returns></returns>
		public static Matrix operator +(Matrix AMat, Matrix BMat)
		{
			if((AMat.NrColumns != BMat.NrColumns) || (AMat.NrRows != BMat.NrRows))
				throw(new MatrixException("The matrix cannot be summed."));

			Matrix mat = new Matrix(AMat.NrRows, AMat.NrColumns);
			for(int i = 0; i < AMat.NrRows; i++)
				for(int j = 0; j < AMat.NrColumns; j++)
					mat[i, j] = AMat[i, j] + BMat[i, j];
			return mat;
		}

		/// <summary>
		/// Substract two matrix
		/// </summary>
		/// <param name="AMat"></param>
		/// <param name="BMat"></param>
		/// <returns></returns>
		public static Matrix operator -(Matrix AMat, Matrix BMat)
		{
			if((AMat.NrColumns != BMat.NrColumns) || (AMat.NrRows != BMat.NrRows))
				throw(new MatrixException("The matrix cannot be summed."));

			Matrix mat = new Matrix(AMat.NrRows, AMat.NrColumns);
			for(int i = 0; i < AMat.NrRows; i++)
				for(int j = 0; j < AMat.NrColumns; j++)
					mat[i, j] = AMat[i, j] - BMat[i, j];
			return mat;
		}

		/// <summary>
		/// Compute the determinant of a square matrix
		/// </summary>
		/// <returns></returns>
		public float Determinant()
		{
			if(m_Rows != m_Columns)
				throw(new MatrixException("The matrix is not sqare."));

			float det = 1.0F;
			Matrix mat = this.GausTrangularization();
			for(int i = 0; i < m_Rows; i++)
				det *= mat[i, i];

			return det * iDF;
		}

		/// <summary>
		/// Compute the inverse of the matrix
		/// The matrix must be square
		/// </summary>
		public Matrix Inverse()
		{
			float det;
			
			// the # rows and # columns must identical
			if(m_Rows != m_Columns)
				throw(new MatrixException("The matrix cannot be inverted."));

			det = this.Determinant();
			// the determinant cannot be 0
			if(det == 0.0F)
				throw(new MatrixException("The determinant cannot be 0."));
			
			Matrix mm = this.Adjoint();
			Matrix mat = new Matrix(m_Rows, m_Rows);
			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Rows; j++)
					mat[i, j] = mm[i, j] / det;

			return mat;			
		}

		/// <summary>
		/// Transpose a matrix
		/// </summary>
		/// <returns></returns>
		public Matrix Transposed()
		{
			Matrix tMat = new Matrix(m_Columns, m_Rows);
			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Columns; j++)
					tMat[j, i] = this[i, j];
			return tMat;
		}

		/// <summary>
		/// Clone the current matrix
		/// </summary>
		/// <returns></returns>
		public Matrix Clone()
		{
			Matrix mat = new Matrix(m_Rows, m_Columns);
			for(int i = 0; i < m_Rows; i++)
				for(int j = 0; j < m_Columns; j++)
					mat[i, j] = this[i, j];
			return mat;
		}


		/// <summary>
		/// Get or set the cell value
		/// </summary>
		public float this[int row, int column]
		{
			get
			{
				return GetCells(row, column);
			}
			set
			{
				SetCells(row, column, value);
			}
		}

		/// <summary>
		/// Get the number of rows
		/// </summary>
		public int NrRows
		{
			get
			{
				return m_Rows;
			}
		}

		/// <summary>
		/// Get the number of columns
		/// </summary>
		public int NrColumns
		{
			get
			{
				return m_Columns;
			}
		}

		/// <summary>
		/// Create an identity matrix
		/// </summary>
		/// <param name="len">numer of rows = columns</param>
		/// <returns></returns>
		public static Matrix Identity(int len)
		{
			Matrix mat = new Matrix(len, len);
			for(int i = 0; i < len; i++)
				for(int j = 0; j < len; j++)
					mat[i,j] = i == j ? 1.0F : 0.0F;
			return mat;
		}
	}
}
