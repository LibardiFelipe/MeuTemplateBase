﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemplateBase.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DefaultMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DefaultMessages() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TemplateBase.Domain.Resources.DefaultMessages", typeof(DefaultMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O campo de {0} está em um formato inválido!.
        /// </summary>
        public static string CampoInvalido {
            get {
                return ResourceManager.GetString("CampoInvalido", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O campo de {0} é obrigatório..
        /// </summary>
        public static string CampoObrigatorio {
            get {
                return ResourceManager.GetString("CampoObrigatorio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O email informado já se encontra cadastrado..
        /// </summary>
        public static string EmailJaExistente {
            get {
                return ResourceManager.GetString("EmailJaExistente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O id inserido não é um guid válido!.
        /// </summary>
        public static string Entidade_IdentificadorInvalido {
            get {
                return ResourceManager.GetString("Entidade_IdentificadorInvalido", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a A query não foi executada!.
        /// </summary>
        public static string Hander_FalhaAoExecutarQuery {
            get {
                return ResourceManager.GetString("Hander_FalhaAoExecutarQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O comando foi executado com sucesso!.
        /// </summary>
        public static string Handler_ComandoExecutado {
            get {
                return ResourceManager.GetString("Handler_ComandoExecutado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O comando é inválido!.
        /// </summary>
        public static string Handler_ComandoInvalido {
            get {
                return ResourceManager.GetString("Handler_ComandoInvalido", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a O comando não foi executado!.
        /// </summary>
        public static string Handler_FalhaAoExecutarComando {
            get {
                return ResourceManager.GetString("Handler_FalhaAoExecutarComando", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a A query foi executada com sucesso!.
        /// </summary>
        public static string Handler_QueryExecutada {
            get {
                return ResourceManager.GetString("Handler_QueryExecutada", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a A query é inválida!.
        /// </summary>
        public static string Handler_QueryInvalida {
            get {
                return ResourceManager.GetString("Handler_QueryInvalida", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Um erro interno ocorreu durante a execução do serviço!.
        /// </summary>
        public static string Service_InternalError {
            get {
                return ResourceManager.GetString("Service_InternalError", resourceCulture);
            }
        }
    }
}
