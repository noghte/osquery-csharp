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
public partial class InternalOptionInfo : TBase
{
  private string _value;
  private string _default_value;
  private string _type;

  public string Value
  {
    get
    {
      return _value;
    }
    set
    {
      __isset.@value = true;
      this._value = value;
    }
  }

  public string Default_value
  {
    get
    {
      return _default_value;
    }
    set
    {
      __isset.default_value = true;
      this._default_value = value;
    }
  }

  public string Type
  {
    get
    {
      return _type;
    }
    set
    {
      __isset.type = true;
      this._type = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool @value;
    public bool default_value;
    public bool type;
  }

  public InternalOptionInfo() {
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
            if (field.Type == TType.String) {
              Value = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              Default_value = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              Type = iprot.ReadString();
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
      TStruct struc = new TStruct("InternalOptionInfo");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Value != null && __isset.@value) {
        field.Name = "value";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Value);
        oprot.WriteFieldEnd();
      }
      if (Default_value != null && __isset.default_value) {
        field.Name = "default_value";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Default_value);
        oprot.WriteFieldEnd();
      }
      if (Type != null && __isset.type) {
        field.Name = "type";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Type);
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
    StringBuilder __sb = new StringBuilder("InternalOptionInfo(");
    bool __first = true;
    if (Value != null && __isset.@value) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Value: ");
      __sb.Append(Value);
    }
    if (Default_value != null && __isset.default_value) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Default_value: ");
      __sb.Append(Default_value);
    }
    if (Type != null && __isset.type) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Type: ");
      __sb.Append(Type);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}
