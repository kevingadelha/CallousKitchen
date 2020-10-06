﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccountService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerializableKitchen", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class SerializableKitchen : object
    {
        
        private int IdField;
        
        private AccountService.SerializableFood[] InventoryField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AccountService.SerializableFood[] Inventory
        {
            get
            {
                return this.InventoryField;
            }
            set
            {
                this.InventoryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerializableFood", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class SerializableFood : object
    {
        
        private string BarcodeField;
        
        private System.Nullable<System.DateTime> ExpiryDateField;
        
        private int IdField;
        
        private string NameField;
        
        private double QuantityField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Barcode
        {
            get
            {
                return this.BarcodeField;
            }
            set
            {
                this.BarcodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ExpiryDate
        {
            get
            {
                return this.ExpiryDateField;
            }
            set
            {
                this.ExpiryDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Quantity
        {
            get
            {
                return this.QuantityField;
            }
            set
            {
                this.QuantityField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerializedFoodFactsProductModel", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Models")]
    public partial class SerializedFoodFactsProductModel : object
    {
        
        private string Image_UrlField;
        
        private string[] IngredientsField;
        
        private string NameField;
        
        private System.Collections.Generic.Dictionary<string, string> NutrientsField;
        
        private int ServingQuantityField;
        
        private string ServingSizeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image_Url
        {
            get
            {
                return this.Image_UrlField;
            }
            set
            {
                this.Image_UrlField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Ingredients
        {
            get
            {
                return this.IngredientsField;
            }
            set
            {
                this.IngredientsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, string> Nutrients
        {
            get
            {
                return this.NutrientsField;
            }
            set
            {
                this.NutrientsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ServingQuantity
        {
            get
            {
                return this.ServingQuantityField;
            }
            set
            {
                this.ServingQuantityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServingSize
        {
            get
            {
                return this.ServingSizeField;
            }
            set
            {
                this.ServingSizeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerializableUser", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class SerializableUser : object
    {
        
        private string[] DietTagsField;
        
        private string EmailField;
        
        private int GuiltLevelField;
        
        private int IdField;
        
        private AccountService.SerializableKitchen[] KitchensField;
        
        private string PasswordField;
        
        private string UsernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] DietTags
        {
            get
            {
                return this.DietTagsField;
            }
            set
            {
                this.DietTagsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GuiltLevel
        {
            get
            {
                return this.GuiltLevelField;
            }
            set
            {
                this.GuiltLevelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AccountService.SerializableKitchen[] Kitchens
        {
            get
            {
                return this.KitchensField;
            }
            set
            {
                this.KitchensField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username
        {
            get
            {
                return this.UsernameField;
            }
            set
            {
                this.UsernameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class User : object
    {
        
        private string[] DietTagsField;
        
        private string EmailField;
        
        private int GuiltLevelField;
        
        private int IdField;
        
        private AccountService.Kitchen[] KitchensField;
        
        private string PasswordField;
        
        private string UsernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] DietTags
        {
            get
            {
                return this.DietTagsField;
            }
            set
            {
                this.DietTagsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GuiltLevel
        {
            get
            {
                return this.GuiltLevelField;
            }
            set
            {
                this.GuiltLevelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AccountService.Kitchen[] Kitchens
        {
            get
            {
                return this.KitchensField;
            }
            set
            {
                this.KitchensField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username
        {
            get
            {
                return this.UsernameField;
            }
            set
            {
                this.UsernameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Kitchen", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class Kitchen : object
    {
        
        private int IdField;
        
        private AccountService.Food[] InventoryField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AccountService.Food[] Inventory
        {
            get
            {
                return this.InventoryField;
            }
            set
            {
                this.InventoryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Food", Namespace="http://schemas.datacontract.org/2004/07/Capstone.Classes")]
    public partial class Food : object
    {
        
        private string BarcodeField;
        
        private System.Nullable<System.DateTime> ExpiryDateField;
        
        private int IdField;
        
        private string NameField;
        
        private double QuantityField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Barcode
        {
            get
            {
                return this.BarcodeField;
            }
            set
            {
                this.BarcodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ExpiryDate
        {
            get
            {
                return this.ExpiryDateField;
            }
            set
            {
                this.ExpiryDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Quantity
        {
            get
            {
                return this.QuantityField;
            }
            set
            {
                this.QuantityField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccountService.IAccountServiceMvc")]
    public interface IAccountServiceMvc
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/CreateAccountWithEmail", ReplyAction="http://tempuri.org/IAccountServiceMvc/CreateAccountWithEmailResponse")]
        int CreateAccountWithEmail(string userName, string pass, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/CreateAccountWithEmail", ReplyAction="http://tempuri.org/IAccountServiceMvc/CreateAccountWithEmailResponse")]
        System.Threading.Tasks.Task<int> CreateAccountWithEmailAsync(string userName, string pass, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/CreateAccount", ReplyAction="http://tempuri.org/IAccountServiceMvc/CreateAccountResponse")]
        int CreateAccount(string userName, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/CreateAccount", ReplyAction="http://tempuri.org/IAccountServiceMvc/CreateAccountResponse")]
        System.Threading.Tasks.Task<int> CreateAccountAsync(string userName, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/LoginAccount", ReplyAction="http://tempuri.org/IAccountServiceMvc/LoginAccountResponse")]
        int LoginAccount(string userName, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/LoginAccount", ReplyAction="http://tempuri.org/IAccountServiceMvc/LoginAccountResponse")]
        System.Threading.Tasks.Task<int> LoginAccountAsync(string userName, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/AddKitchen", ReplyAction="http://tempuri.org/IAccountServiceMvc/AddKitchenResponse")]
        int AddKitchen(int userId, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/AddKitchen", ReplyAction="http://tempuri.org/IAccountServiceMvc/AddKitchenResponse")]
        System.Threading.Tasks.Task<int> AddKitchenAsync(int userId, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetKitchens", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetKitchensResponse")]
        AccountService.SerializableKitchen[] GetKitchens(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetKitchens", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetKitchensResponse")]
        System.Threading.Tasks.Task<AccountService.SerializableKitchen[]> GetKitchensAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetInventory", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetInventoryResponse")]
        AccountService.SerializableFood[] GetInventory(int kitchenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetInventory", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetInventoryResponse")]
        System.Threading.Tasks.Task<AccountService.SerializableFood[]> GetInventoryAsync(int kitchenId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetBarcodeData", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetBarcodeDataResponse")]
        string GetBarcodeData(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetBarcodeData", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetBarcodeDataResponse")]
        System.Threading.Tasks.Task<string> GetBarcodeDataAsync(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetAllOpenFoodFacts", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetAllOpenFoodFactsResponse")]
        AccountService.SerializedFoodFactsProductModel GetAllOpenFoodFacts(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetAllOpenFoodFacts", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetAllOpenFoodFactsResponse")]
        System.Threading.Tasks.Task<AccountService.SerializedFoodFactsProductModel> GetAllOpenFoodFactsAsync(string barcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/AddFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/AddFoodResponse")]
        bool AddFood(int kitchenId, string name, int quantity, System.Nullable<System.DateTime> expiryDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/AddFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/AddFoodResponse")]
        System.Threading.Tasks.Task<bool> AddFoodAsync(int kitchenId, string name, int quantity, System.Nullable<System.DateTime> expiryDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/EatFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/EatFoodResponse")]
        bool EatFood(int id, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/EatFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/EatFoodResponse")]
        System.Threading.Tasks.Task<bool> EatFoodAsync(int id, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/EditFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/EditFoodResponse")]
        bool EditFood(int id, string name, int quantity, System.Nullable<System.DateTime> expiryDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/EditFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/EditFoodResponse")]
        System.Threading.Tasks.Task<bool> EditFoodAsync(int id, string name, int quantity, System.Nullable<System.DateTime> expiryDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/RemoveItem", ReplyAction="http://tempuri.org/IAccountServiceMvc/RemoveItemResponse")]
        bool RemoveItem(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/RemoveItem", ReplyAction="http://tempuri.org/IAccountServiceMvc/RemoveItemResponse")]
        System.Threading.Tasks.Task<bool> RemoveItemAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/Test", ReplyAction="http://tempuri.org/IAccountServiceMvc/TestResponse")]
        AccountService.SerializableUser[] Test();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/Test", ReplyAction="http://tempuri.org/IAccountServiceMvc/TestResponse")]
        System.Threading.Tasks.Task<AccountService.SerializableUser[]> TestAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetSerializableUser", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetSerializableUserResponse")]
        AccountService.SerializableUser GetSerializableUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetSerializableUser", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetSerializableUserResponse")]
        System.Threading.Tasks.Task<AccountService.SerializableUser> GetSerializableUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetUser", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetUserResponse")]
        AccountService.User GetUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetUser", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetUserResponse")]
        System.Threading.Tasks.Task<AccountService.User> GetUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetFoodResponse")]
        AccountService.Food GetFood(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountServiceMvc/GetFood", ReplyAction="http://tempuri.org/IAccountServiceMvc/GetFoodResponse")]
        System.Threading.Tasks.Task<AccountService.Food> GetFoodAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IAccountServiceMvcChannel : AccountService.IAccountServiceMvc, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class AccountServiceMvcClient : System.ServiceModel.ClientBase<AccountService.IAccountServiceMvc>, AccountService.IAccountServiceMvc
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AccountServiceMvcClient() : 
                base(AccountServiceMvcClient.GetDefaultBinding(), AccountServiceMvcClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IAccountServiceMvc.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AccountServiceMvcClient(EndpointConfiguration endpointConfiguration) : 
                base(AccountServiceMvcClient.GetBindingForEndpoint(endpointConfiguration), AccountServiceMvcClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AccountServiceMvcClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AccountServiceMvcClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AccountServiceMvcClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AccountServiceMvcClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AccountServiceMvcClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public int CreateAccountWithEmail(string userName, string pass, string email)
        {
            return base.Channel.CreateAccountWithEmail(userName, pass, email);
        }
        
        public System.Threading.Tasks.Task<int> CreateAccountWithEmailAsync(string userName, string pass, string email)
        {
            return base.Channel.CreateAccountWithEmailAsync(userName, pass, email);
        }
        
        public int CreateAccount(string userName, string pass)
        {
            return base.Channel.CreateAccount(userName, pass);
        }
        
        public System.Threading.Tasks.Task<int> CreateAccountAsync(string userName, string pass)
        {
            return base.Channel.CreateAccountAsync(userName, pass);
        }
        
        public int LoginAccount(string userName, string pass)
        {
            return base.Channel.LoginAccount(userName, pass);
        }
        
        public System.Threading.Tasks.Task<int> LoginAccountAsync(string userName, string pass)
        {
            return base.Channel.LoginAccountAsync(userName, pass);
        }
        
        public int AddKitchen(int userId, string name)
        {
            return base.Channel.AddKitchen(userId, name);
        }
        
        public System.Threading.Tasks.Task<int> AddKitchenAsync(int userId, string name)
        {
            return base.Channel.AddKitchenAsync(userId, name);
        }
        
        public AccountService.SerializableKitchen[] GetKitchens(int userId)
        {
            return base.Channel.GetKitchens(userId);
        }
        
        public System.Threading.Tasks.Task<AccountService.SerializableKitchen[]> GetKitchensAsync(int userId)
        {
            return base.Channel.GetKitchensAsync(userId);
        }
        
        public AccountService.SerializableFood[] GetInventory(int kitchenId)
        {
            return base.Channel.GetInventory(kitchenId);
        }
        
        public System.Threading.Tasks.Task<AccountService.SerializableFood[]> GetInventoryAsync(int kitchenId)
        {
            return base.Channel.GetInventoryAsync(kitchenId);
        }
        
        public string GetBarcodeData(string barcode)
        {
            return base.Channel.GetBarcodeData(barcode);
        }
        
        public System.Threading.Tasks.Task<string> GetBarcodeDataAsync(string barcode)
        {
            return base.Channel.GetBarcodeDataAsync(barcode);
        }
        
        public AccountService.SerializedFoodFactsProductModel GetAllOpenFoodFacts(string barcode)
        {
            return base.Channel.GetAllOpenFoodFacts(barcode);
        }
        
        public System.Threading.Tasks.Task<AccountService.SerializedFoodFactsProductModel> GetAllOpenFoodFactsAsync(string barcode)
        {
            return base.Channel.GetAllOpenFoodFactsAsync(barcode);
        }
        
        public bool AddFood(int kitchenId, string name, int quantity, System.Nullable<System.DateTime> expiryDate)
        {
            return base.Channel.AddFood(kitchenId, name, quantity, expiryDate);
        }
        
        public System.Threading.Tasks.Task<bool> AddFoodAsync(int kitchenId, string name, int quantity, System.Nullable<System.DateTime> expiryDate)
        {
            return base.Channel.AddFoodAsync(kitchenId, name, quantity, expiryDate);
        }
        
        public bool EatFood(int id, int quantity)
        {
            return base.Channel.EatFood(id, quantity);
        }
        
        public System.Threading.Tasks.Task<bool> EatFoodAsync(int id, int quantity)
        {
            return base.Channel.EatFoodAsync(id, quantity);
        }
        
        public bool EditFood(int id, string name, int quantity, System.Nullable<System.DateTime> expiryDate)
        {
            return base.Channel.EditFood(id, name, quantity, expiryDate);
        }
        
        public System.Threading.Tasks.Task<bool> EditFoodAsync(int id, string name, int quantity, System.Nullable<System.DateTime> expiryDate)
        {
            return base.Channel.EditFoodAsync(id, name, quantity, expiryDate);
        }
        
        public bool RemoveItem(int id)
        {
            return base.Channel.RemoveItem(id);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveItemAsync(int id)
        {
            return base.Channel.RemoveItemAsync(id);
        }
        
        public AccountService.SerializableUser[] Test()
        {
            return base.Channel.Test();
        }
        
        public System.Threading.Tasks.Task<AccountService.SerializableUser[]> TestAsync()
        {
            return base.Channel.TestAsync();
        }
        
        public AccountService.SerializableUser GetSerializableUser(int id)
        {
            return base.Channel.GetSerializableUser(id);
        }
        
        public System.Threading.Tasks.Task<AccountService.SerializableUser> GetSerializableUserAsync(int id)
        {
            return base.Channel.GetSerializableUserAsync(id);
        }
        
        public AccountService.User GetUser(int id)
        {
            return base.Channel.GetUser(id);
        }
        
        public System.Threading.Tasks.Task<AccountService.User> GetUserAsync(int id)
        {
            return base.Channel.GetUserAsync(id);
        }
        
        public AccountService.Food GetFood(int id)
        {
            return base.Channel.GetFood(id);
        }
        
        public System.Threading.Tasks.Task<AccountService.Food> GetFoodAsync(int id)
        {
            return base.Channel.GetFoodAsync(id);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAccountServiceMvc))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAccountServiceMvc))
            {
                return new System.ServiceModel.EndpointAddress("http://callouskitchen.canadaeast.cloudapp.azure.com:8080/AccountServiceMvc.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return AccountServiceMvcClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IAccountServiceMvc);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return AccountServiceMvcClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IAccountServiceMvc);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IAccountServiceMvc,
        }
    }
}
