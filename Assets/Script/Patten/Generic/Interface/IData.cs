namespace Study.Patten.Generic
{
    public interface IData
    {
        public void Update(IData data);

        public T GetData<T>() where T : class, IData;
    }
}