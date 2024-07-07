using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.StaticModels;

public class BaseModel<T>
{
    public int code { get; set; }
    public string message { get; set; }
    public T Data { get; set; }
}
