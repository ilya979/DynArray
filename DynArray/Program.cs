// See https://aka.ms/new-console-template for more information
//SingleArray<int> iArray = new SingleArray<int>();
//VectorArray<int> iArray = new VectorArray<int>();
//FactorArray<int> iArray = new FactorArray<int>();
MatrixArray<int> iArray = new MatrixArray<int>();
int N1;

do
{
    N1 = Convert.ToInt32(Console.ReadLine());
    iArray.put(N1, 0);
} while (N1 != 0);

for(int i = 0; i < iArray.size(); i++)
{
    Console.WriteLine(iArray.get(i));
}

do
{
    Console.WriteLine(iArray.remove(0));
} while(iArray.size() > 0);

//do
//{
//    N1 = Convert.ToInt32(Console.ReadLine());
//    iArray.put(N1, 0);
//} while (N1 != 0);

//for (int i = 0; i < iArray.size(); i++)
//{
//    Console.WriteLine(iArray.get(i));
//}


class MatrixArray<T>
{
    SingleArray<VectorArray<T>> vArray = new SingleArray<VectorArray<T>>();
    VectorArray<T> tmpRow;
    int hSize;

    public MatrixArray()
    {
        hSize = 5;
        VectorArray<T> row = new VectorArray<T>(hSize);
        vArray.put(row);
    }

    public MatrixArray(int width)
    {
        hSize = width;
        VectorArray<T> row = new VectorArray<T>(hSize);
        vArray.put(row);
    }
    public T get(int index)
    {
        tmpRow = vArray.get(index / hSize);
        return tmpRow.get(index % hSize);
    }

    public void put(T item)
    {
        tmpRow = vArray.get(vArray.size()-1);
        if (tmpRow.size() == hSize)
        {
            VectorArray<T> row = new VectorArray<T>(hSize);
            row.put(item);
            vArray.put(row);
        }
        else
        {
            tmpRow.put(item);
        }
        return;
    }
    public void put(T item, int index)
    {
        T tmpItem;
        bool bTransfer;
        int newRow = index / hSize;
        int newCol = index % hSize;

        tmpRow = vArray.get(newRow);

        do
        {
            // перенос требуется если длина текущей строки уже максимальна
            bTransfer = (tmpRow.size() == hSize);
            if (bTransfer)
            {
                tmpItem = tmpRow.remove(hSize - 1);
                tmpRow.put(item, newCol);
                item = tmpItem;
                newCol = 0;
                newRow++;
                if (newRow == vArray.size())
                {
                    VectorArray<T> row = new VectorArray<T>(hSize);
                    row.put(item);
                    vArray.put(row);
                    bTransfer = false;
                }
                else
                {
                    tmpRow = vArray.get(newRow);
                }
            }
            else
            {
                tmpRow.put(item, newCol);
            }
        } while (bTransfer);

        return;
    }
    public T remove(int index)
    {
        T tmpItem;
        T resItem;
        bool bTransfer;
        int cRow = index / hSize;
        int cCol = index % hSize;
        VectorArray<T> tmpRow2;

        tmpRow = vArray.get(cRow);

        resItem = tmpRow.remove(cCol);

        bTransfer = cRow < (vArray.size() - 1);

        while (bTransfer) {
            tmpRow2 = vArray.get(++cRow);
            tmpRow.put(tmpRow2.remove(0));
            tmpRow = tmpRow2;
            bTransfer = (cRow != (vArray.size() - 1));
        }
        if (tmpRow.size() == 0)
        {
            // не удаляем последнюю строку
            if (vArray.size() > 1)
            {
                tmpRow = vArray.remove(vArray.size() - 1);
            }
        }

        return resItem;

    }

    public int size()
    {
        int vSize = vArray.size();
        int res = 0;
        if (vSize == 0)
        {
            res = 0;
        }
        else
        {
            tmpRow = vArray.get(vArray.size() - 1);
            res = (vArray.size() - 1) * hSize + tmpRow.size();
        }
        return res;
    }

    public Boolean isEmpty()
    {
        return (size() == 0);
    }
}

class FactorArray<T>
{
    int arraySize;
    int realSize;
    double multStep;
    T[] array;
    public T get(int index)
    {
        return array[index];
    }

    public FactorArray()
    {
        multStep = 2;
        arraySize = 0;
        realSize = 5;
        array = new T[realSize];
    }

    public FactorArray(double step)
    {
        multStep = step;
        arraySize = 0;
        realSize = 5;
        array = new T[realSize];
    }

    public void put(T item)
    {
        if (realSize == arraySize)
        {
            realSize = Convert.ToInt32(Math.Truncate(multStep * realSize));
            T[] newArray = new T[realSize];
            for (int i = 0; i < arraySize; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        array[arraySize] = item;

        arraySize += 1;
        return;
    }
    public void put(T item, int index)
    {
        if (realSize == arraySize)
        {
            realSize = Convert.ToInt32(Math.Truncate(multStep * realSize));
            T[] newArray = new T[realSize];
            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }
            newArray[index] = item;
            for (int i = index; i < arraySize; i++)
            {
                newArray[i + 1] = array[i];
            }
            arraySize++;
            array = newArray;
        }
        else
        {
            for (int i = arraySize; i > index; i--)
            {
                array[i] = array[i - 1];
            }
            array[index] = item;
            arraySize++;
        }
        return;

    }

    public T remove(int index)
    {
        T item = array[index];
        for (int i = index + 1; i < arraySize; i++)
        {
            array[i - 1] = array[i];
        }
        arraySize--;
        return item;
    }

    public int size()
    {
        return arraySize;
    }

    public Boolean isEmpty()
    {
        return (size() == 0);
    }
}

class VectorArray<T>
{
    int arraySize;
    int realSize;
    int incStep;
    T[] array;
    public T get(int index)
    {
        return array[index];
    }

    public VectorArray()
    {
        incStep = 5;
        arraySize = 0;
        realSize = 0;
    }

    public VectorArray(int step)
    {
        incStep = step;
        arraySize = 0;
        realSize = 0;
    }

    public void put(T item)
    {
        if(realSize == arraySize) {
            realSize += incStep;
            T[] newArray = new T[realSize];
            for (int i = 0; i < arraySize; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        array[arraySize] = item;

        arraySize += 1;
        return;
    }
    public void put(T item, int index)
    {
        if (realSize == arraySize)
        {
            realSize += incStep;
            T[] newArray = new T[realSize];
            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }
            newArray[index] = item;
            for (int i = index; i < arraySize; i++)
            {
                newArray[i + 1] = array[i];
            }
            arraySize++;
            array = newArray;
        }
        else
        {
            for(int i = arraySize; i > index; i--)
            {
                array[i] = array[i-1];
            }
            array[index] = item;
            arraySize++;
        }
        return;
    }

    public T remove(int index)
    {
        T item = array[index];
        for (int i = index + 1; i < arraySize; i++)
        {
            array[i - 1] = array[i];
        }
        arraySize--;
        return item;
    }

    public int size()
    {
        return arraySize;
    }

    public Boolean isEmpty()
    {
        return (size() == 0);
    }
}

class SingleArray<T>
{
    int arraySize;
    T[] array;  
    public T get(int index) 
    {
        return array[index];
    }

    public SingleArray()
    {
        arraySize = 0;
        array = new T[arraySize];
    }
    public void put(T item)
    {
        T[] newArray = new T[arraySize+1];

        for(int i=0;i< arraySize; i++)
        {
            newArray[i] = array[i];
        }
        newArray[arraySize] = item;

        arraySize++;
        array = newArray;
        return;
    }
    public void put(T item, int index)
    {
        T[] newArray = new T[arraySize + 1];
        for (int i = 0; i < index; i++)
        {
            newArray[i] = array[i];
        }
        newArray[index] = item;
        for(int i = index; i<arraySize; i++)
        {
            newArray[i+1] = array[i];
        }

        arraySize++;
        array = newArray;
        return;
    }

    public T remove(int index) 
    { 
        T item = array[index];
        T[] newArray = new T[arraySize-1];
        for(int i = 0; i < index; i++)
        {
            newArray[i]=array[i];
        }
        for(int i=index+1;i<arraySize; i++)
        {
            newArray[i-1] = array[i];
        }
        array = newArray;
        arraySize--;
        return item;
    }

    public int size()
    {
        return arraySize;
    }

    public Boolean isEmpty()
    {
        return (size() == 0);
    }
}