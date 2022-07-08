﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace MovieCenter.cr.fi.bccr.gee {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsindicadoreseconomicosSoap", Namespace="http://ws.sdde.bccr.fi.cr")]
    public partial class wsindicadoreseconomicos : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ObtenerIndicadoresEconomicosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ObtenerIndicadoresEconomicosXMLOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsindicadoreseconomicos() {
            this.Url = global::MovieCenter.Properties.Settings.Default.MovieCenter_cr_fi_bccr_gee_wsindicadoreseconomicos;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ObtenerIndicadoresEconomicosCompletedEventHandler ObtenerIndicadoresEconomicosCompleted;
        
        /// <remarks/>
        public event ObtenerIndicadoresEconomicosXMLCompletedEventHandler ObtenerIndicadoresEconomicosXMLCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ws.sdde.bccr.fi.cr/ObtenerIndicadoresEconomicos", RequestNamespace="http://ws.sdde.bccr.fi.cr", ResponseNamespace="http://ws.sdde.bccr.fi.cr", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ObtenerIndicadoresEconomicos(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token) {
            object[] results = this.Invoke("ObtenerIndicadoresEconomicos", new object[] {
                        Indicador,
                        FechaInicio,
                        FechaFinal,
                        Nombre,
                        SubNiveles,
                        CorreoElectronico,
                        Token});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ObtenerIndicadoresEconomicosAsync(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token) {
            this.ObtenerIndicadoresEconomicosAsync(Indicador, FechaInicio, FechaFinal, Nombre, SubNiveles, CorreoElectronico, Token, null);
        }
        
        /// <remarks/>
        public void ObtenerIndicadoresEconomicosAsync(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token, object userState) {
            if ((this.ObtenerIndicadoresEconomicosOperationCompleted == null)) {
                this.ObtenerIndicadoresEconomicosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnObtenerIndicadoresEconomicosOperationCompleted);
            }
            this.InvokeAsync("ObtenerIndicadoresEconomicos", new object[] {
                        Indicador,
                        FechaInicio,
                        FechaFinal,
                        Nombre,
                        SubNiveles,
                        CorreoElectronico,
                        Token}, this.ObtenerIndicadoresEconomicosOperationCompleted, userState);
        }
        
        private void OnObtenerIndicadoresEconomicosOperationCompleted(object arg) {
            if ((this.ObtenerIndicadoresEconomicosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ObtenerIndicadoresEconomicosCompleted(this, new ObtenerIndicadoresEconomicosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ws.sdde.bccr.fi.cr/ObtenerIndicadoresEconomicosXML", RequestNamespace="http://ws.sdde.bccr.fi.cr", ResponseNamespace="http://ws.sdde.bccr.fi.cr", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ObtenerIndicadoresEconomicosXML(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token) {
            object[] results = this.Invoke("ObtenerIndicadoresEconomicosXML", new object[] {
                        Indicador,
                        FechaInicio,
                        FechaFinal,
                        Nombre,
                        SubNiveles,
                        CorreoElectronico,
                        Token});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ObtenerIndicadoresEconomicosXMLAsync(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token) {
            this.ObtenerIndicadoresEconomicosXMLAsync(Indicador, FechaInicio, FechaFinal, Nombre, SubNiveles, CorreoElectronico, Token, null);
        }
        
        /// <remarks/>
        public void ObtenerIndicadoresEconomicosXMLAsync(string Indicador, string FechaInicio, string FechaFinal, string Nombre, string SubNiveles, string CorreoElectronico, string Token, object userState) {
            if ((this.ObtenerIndicadoresEconomicosXMLOperationCompleted == null)) {
                this.ObtenerIndicadoresEconomicosXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnObtenerIndicadoresEconomicosXMLOperationCompleted);
            }
            this.InvokeAsync("ObtenerIndicadoresEconomicosXML", new object[] {
                        Indicador,
                        FechaInicio,
                        FechaFinal,
                        Nombre,
                        SubNiveles,
                        CorreoElectronico,
                        Token}, this.ObtenerIndicadoresEconomicosXMLOperationCompleted, userState);
        }
        
        private void OnObtenerIndicadoresEconomicosXMLOperationCompleted(object arg) {
            if ((this.ObtenerIndicadoresEconomicosXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ObtenerIndicadoresEconomicosXMLCompleted(this, new ObtenerIndicadoresEconomicosXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void ObtenerIndicadoresEconomicosCompletedEventHandler(object sender, ObtenerIndicadoresEconomicosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ObtenerIndicadoresEconomicosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ObtenerIndicadoresEconomicosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void ObtenerIndicadoresEconomicosXMLCompletedEventHandler(object sender, ObtenerIndicadoresEconomicosXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ObtenerIndicadoresEconomicosXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ObtenerIndicadoresEconomicosXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591