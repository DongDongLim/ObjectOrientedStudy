namespace Study.Patten.Common
{
    public interface IData
    {
        public void Update(IData data);

        public T GetData<T>() where T : IData;
    }
}