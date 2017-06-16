using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Championship.Service
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();

        MobileServiceUser User { get; }

        Task<string> UploadImage(Stream photo);
	}
}
