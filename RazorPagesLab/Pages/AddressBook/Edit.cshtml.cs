using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IRepo<AddressBookEntry> _repo;

    public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    [BindProperty] public UpdateAddressRequest UpdateAddressRequest { get; set; }

    public void OnGet(Guid id)
    {
        var existingRecord = _repo.FindById(id);

        if (existingRecord != null)
        {
            this.UpdateAddressRequest = new UpdateAddressRequest()
            {
                City = existingRecord.City,
                Id = existingRecord.Id,
                Line1 = existingRecord.Line1,
                Line2 = existingRecord.Line2,
                State = existingRecord.State,
                PostalCode = existingRecord.PostalCode
            };
        }
    }

    public async Task<ActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _ = await _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }

        return Page();
    }
}