// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.1
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Streaming.Joao
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.11.1")]
	public partial class Order : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse(@"{""type"":""record"",""name"":""Order"",""namespace"":""Streaming.Joao"",""fields"":[{""name"":""userId"",""doc"":""unique identifier of the user"",""type"":[""null"",""string""],""defalut"":null},{""name"":""orderId"",""doc"":""unique identifier of the order"",""type"":[""null"",""string""],""defalut"":null},{""name"":""amount"",""doc"":""total value of the order"",""type"":[""null"",""double""],""defalut"":null}],""title"":""ECOMMERCE_NEW_ORDER"",""description"":""This is an Scheme for making orders""}");
		/// <summary>
		/// unique identifier of the user
		/// </summary>
		private string _userId;
		/// <summary>
		/// unique identifier of the order
		/// </summary>
		private string _orderId;
		/// <summary>
		/// total value of the order
		/// </summary>
		private System.Nullable<System.Double> _amount;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return Order._SCHEMA;
			}
		}
		/// <summary>
		/// unique identifier of the user
		/// </summary>
		public string userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}
		/// <summary>
		/// unique identifier of the order
		/// </summary>
		public string orderId
		{
			get
			{
				return this._orderId;
			}
			set
			{
				this._orderId = value;
			}
		}
		/// <summary>
		/// total value of the order
		/// </summary>
		public System.Nullable<System.Double> amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this._amount = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.userId;
			case 1: return this.orderId;
			case 2: return this.amount;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.userId = (System.String)fieldValue; break;
			case 1: this.orderId = (System.String)fieldValue; break;
			case 2: this.amount = (System.Nullable<System.Double>)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}