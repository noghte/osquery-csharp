/**
 * Autogenerated by Thrift Compiler (0.11.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


#if !SILVERLIGHT
[Serializable]
#endif
public partial class ExtensionResponse : TBase
{
  private ExtensionStatus _status;
  private List<Dictionary<string, string>> _response;

  public ExtensionStatus Status
  {
    get
    {
      return _status;
    }
    set
    {
      __isset.status = true;
      this._status = value;
    }
  }

  public List<Dictionary<string, string>> Response
  {
    get
    {
      return _response;
    }
    set
    {
      __isset.response = true;
      this._response = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool status;
    public bool response;
  }

  public ExtensionResponse() {
  }

  public void Read (TProtocol iprot)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.Struct) {
              Status = new ExtensionStatus();
              Status.Read(iprot);
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.List) {
              {
                Response = new List<Dictionary<string, string>>();
                TList _list0 = iprot.ReadListBegin();
                for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                {
                  Dictionary<string, string> _elem2;
                  {
                    _elem2 = new Dictionary<string, string>();
                    TMap _map3 = iprot.ReadMapBegin();
                    for( int _i4 = 0; _i4 < _map3.Count; ++_i4)
                    {
                      string _key5;
                      string _val6;
                      _key5 = iprot.ReadString();
                      _val6 = iprot.ReadString();
                      _elem2[_key5] = _val6;
                    }
                    iprot.ReadMapEnd();
                  }
                  Response.Add(_elem2);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public void Write(TProtocol oprot) {
    oprot.IncrementRecursionDepth();
    try
    {
      TStruct struc = new TStruct("ExtensionResponse");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Status != null && __isset.status) {
        field.Name = "status";
        field.Type = TType.Struct;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        Status.Write(oprot);
        oprot.WriteFieldEnd();
      }
      if (Response != null && __isset.response) {
        field.Name = "response";
        field.Type = TType.List;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Map, Response.Count));
          foreach (Dictionary<string, string> _iter7 in Response)
          {
            {
              oprot.WriteMapBegin(new TMap(TType.String, TType.String, _iter7.Count));
              foreach (string _iter8 in _iter7.Keys)
              {
                oprot.WriteString(_iter8);
                oprot.WriteString(_iter7[_iter8]);
              }
              oprot.WriteMapEnd();
            }
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override string ToString() {
    StringBuilder __sb = new StringBuilder("ExtensionResponse(");
    bool __first = true;
    if (Status != null && __isset.status) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Status: ");
      __sb.Append(Status== null ? "<null>" : Status.ToString());
    }
    if (Response != null && __isset.response) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Response: ");
      __sb.Append(Response);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}

