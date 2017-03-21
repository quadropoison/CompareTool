namespace CompareTool
{
    public interface IComparator
    {
        bool IsEquals { get; set; }
        void CompareTwoObjects(object itemExpected, object itemActual); 
    }
}