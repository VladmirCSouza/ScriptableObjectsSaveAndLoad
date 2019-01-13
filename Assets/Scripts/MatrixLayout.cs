[System.Serializable]
public class Col
{
    public int[] rows = new int[13];
}

[System.Serializable]
public class MatrixLayout
{
    public Col[] cols = new Col[13];
}
