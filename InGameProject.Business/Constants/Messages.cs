using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductNotFound = "Ürün bulunamadı.";
        public static string ProductDeleted = "Ürün silindi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameCharacterLength = "Ürün adı en az 3 karakter olmalıdır.";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";

        public static string CategoryAdded = "Kategori eklendi";
        public static string CategoryNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryNameCharacterLength = "Kategori adı en az 3 karakter olmalıdır.";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string CategoriesListed = "Kategoriler listelendi";
        public static string CategoryUpdated = "Kategori güncellendi";
        public static string CategoryNotFound = "Kategori bulunamadı.";
        public static string CategoryDeleted = "Kategori silindi.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string PasswordError = "Parola hatası.";
        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string UserAlreadyExists = "Kullanıcı mevcut.";
        public static string CreatedToken = "Token oluşturuldu.";
        public static string NotCreatedToken = "Kimlik doğrulama hatası, token oluşturulamadı.";

        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserUpdated = "Kullanıcı güncellendi"; 
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UsersListed = "Kullanıcıler listelendi"; 
        public static string UserNameCharacterLength = "Kullanıcı adı en az 13 karakter olmalıdır.";
        public static string UserNameAlreadyExists = "Bu isimde zaten başka bir kullanıcı var";
        public static string UserPasswordCreated = "Yeni şifre oluşturuldu.";

        public static string RoleAdded = "Rol eklendi";
        public static string RoleUpdated = "Rol güncellendi";
        public static string RoleNotFound = "Rol bulunamadı.";
        public static string RoleDeleted = "Rol silindi."; 
        public static string RolesListed = "Roller listelendi"; 
        public static string RoleNameCharacterLength = "Rol adı en az 3 karakter olmalıdır.";
        public static string RoleNameAlreadyExists = "Bu isimde zaten başka bir role var";

    }
}
