using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DomainModel
{
	public record Adresse
	{
		[Key]
		public int AdresseID { get; init; }

		[StringLength(50)]
		public required string Street { get; set; }

		[StringLength(100)]
		public required string City { get; set; }

		[StringLength(10)]
		public string? ZipCode { get; set; }

		[StringLength(50)]
		public string? Country { get; set; }

		public int ClientID { get; set; }

		[JsonIgnore]
		public Client? Client { get; set; }
	}
}
