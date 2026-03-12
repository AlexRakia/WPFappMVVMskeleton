using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfMvvmSkeleton.Models;

namespace WpfMvvmSkeleton.Services
{
    // A Service does the actual work: talks to databases, reads files,
    // calls APIs, etc.  It has NO knowledge of the UI whatsoever.
    // The ViewModel calls the Service; the View calls the ViewModel.
    //
    // Tip: define an interface (ISampleService) and inject it into the
    // ViewModel constructor — that makes unit-testing trivial because
    // you can swap in a fake implementation.

    public interface ISampleService
    {
        Task<List<SampleItem>> GetItemsAsync();
        Task<bool>             SaveItemAsync(SampleItem item);
        Task<bool>             DeleteItemAsync(int id);
    }

    public class SampleService : ISampleService
    {
        // ── Replace the bodies below with real DB / API calls ──────────

        public async Task<List<SampleItem>> GetItemsAsync()
        {
            // Simulate async work (e.g. await dbContext.Items.ToListAsync())
            await Task.Delay(200);

            return new List<SampleItem>
            {
                new SampleItem { Id = 1, Name = "Item A", IsActive = true,  CreatedAt = DateTime.Now },
                new SampleItem { Id = 2, Name = "Item B", IsActive = false, CreatedAt = DateTime.Now },
            };
        }

        public async Task<bool> SaveItemAsync(SampleItem item)
        {
            await Task.Delay(100);
            // TODO: persist to DB
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            await Task.Delay(100);
            // TODO: remove from DB
            return true;
        }
    }
}
