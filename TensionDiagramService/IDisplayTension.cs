using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***********************
using System.ServiceModel;
using System.Runtime.Serialization;


namespace TensionDiagramService
{
    [ServiceContract(Namespace = "Tenstenstens")]
    internal interface IDisplayTension
    {
        [OperationContract]
        void ResiveTensionMethod(DataPost datapost);

    } // internal interface IDisplayTension

    [DataContract]
    public class DataPost
    {
        [DataMember]
        double Strain { get; set; }
        [DataMember]
        double Tension { get; set; }
        [DataMember]
        string Testtime { get; set; }

    } //  public class DataPost

} // namespace TensionDiagramService
