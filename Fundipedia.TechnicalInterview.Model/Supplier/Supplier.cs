using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundipedia.TechnicalInterview.Model.Supplier;

public class Supplier : IValidatableObject
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title
    /// </summary>
    [MaxLength(32)]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the first name
    /// </summary>
    [MaxLength(64)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name
    /// </summary>
    [MaxLength(64)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the activation date
    /// </summary>
    public DateTime ActivationDate { get; set; }

    /// <summary>
    /// Gets or sets Emails for the party
    /// </summary>
    public virtual ICollection<Email> Emails { get; set; } = new HashSet<Email>();

    /// <summary>
    /// Gets or sets Phones for the party
    /// </summary>
    public virtual ICollection<Phone> Phones { get; set; } = new HashSet<Phone>();

    /// <summary>
    /// Supplier validation
    /// </summary>
    /// <param name="validationContext"><see cref="ValidationContext"/></param>
    /// <returns>List of <see cref="ValidationResult"/></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();
        return validationResults;
    }
}