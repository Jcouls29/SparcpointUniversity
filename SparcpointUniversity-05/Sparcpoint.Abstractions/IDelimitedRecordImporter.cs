using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sparcpoint.Abstractions
{
    public interface IDelimitedRecordImporter<TRecord>
    {
        Task<IEnumerable<TRecord>> Import(Stream stream, DelimitedRecordOptions options);
    }

    public static class DelimitedRecordImporterExtensions
    {
        public static async Task<IEnumerable<TRecord>> ImportFromFile<TRecord>(this IDelimitedRecordImporter<TRecord> importer, string filePath, DelimitedRecordOptions options)
        {
            if (importer == null)
                throw new ArgumentNullException(nameof(importer));

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await importer.Import(stream, options);
            }
        }

        public static async Task<IEnumerable<TRecord>> ImportFromUri<TRecord>(this IDelimitedRecordImporter<TRecord> importer, Uri downloadPath, DelimitedRecordOptions options)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(downloadPath);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return await importer.Import(stream, options);
                }
            }
        }

        public static async Task<IEnumerable<TRecord>> ImportCsvFromFile<TRecord>(this IDelimitedRecordImporter<TRecord> importer, string filePath)
        {
            return await importer.ImportFromFile(filePath, DelimitedRecordOptions.Default.WithCommaDelimiter());
        }

        public static DelimitedRecordOptions WithTabDelimiter(this DelimitedRecordOptions opt)
        {
            return new DelimitedRecordOptions
            {
                Delimiter = "\t",
                AllowBlankDatums = opt.AllowBlankDatums
            };
        }

        public static DelimitedRecordOptions WithCommaDelimiter(this DelimitedRecordOptions opt)
        {
            return new DelimitedRecordOptions
            {
                Delimiter = ",",
                AllowBlankDatums = opt.AllowBlankDatums
            };
        }
    }

    //public interface IRecordImporter<TRecord>
    //{
    //    Task<IEnumerable<TRecord>> Import(Stream stream);
    //}

    public sealed class DelimitedRecordOptions
    {
        public string Delimiter { get; set; } = ",";
        public bool AllowBlankDatums { get; set; } = true;

        public static DelimitedRecordOptions Default { get; } = new DelimitedRecordOptions();
    }
}
