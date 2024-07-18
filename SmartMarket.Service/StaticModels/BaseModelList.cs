namespace SmartMarket.Desktop.Service.StaticModels;

public class BaseModelList<T>
{
    public int code { get; set; }
    public string message { get; set; }
    public IList<T> Data { get; set; }

}
