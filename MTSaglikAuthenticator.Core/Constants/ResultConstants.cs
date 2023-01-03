using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Core.Constants
{
    public class ResultConstants
    {
        public const string LoginUserInfo = "Giriş yapan kullanıcı bilgisi";
        public const string UserCreated = "Kullanıcı oluşturuldu.";
        public const string UserCreateFail = "Kullanıcı oluşturulurken hata meydana geldi!";
        public const string ModelisEmpty = "Gönderilen model boş olamaz!";

        public const string EditSuccess = "Güncelleme işlemi yapılmıştır.";
        public const string EditFail = "Güncelleme işleminde hata meydana geldi!.";


        public const string PhoneIsExist = "Telefon numarası kayıtlıdır.";
        public const string PhoneIsNotExist = "Telefon numarası kayıtlı değildir!";

        public const string UserIsExistById = "Kullanıcı kayıtlıdır.";
        public const string UserIsNotExistById = "Kullanıcı kayıtlı değildir!";

        public const string UserList = "Kullanıcı listesi";
        public const string UserListNotFound = "Kullanıcı listesi bulunamadı!";

        public const string DeleteOk = "Silme İşlemi tamamlandı";
        public const string DeleteNotOk = "Silme işlemi başarısız";






    }
}
