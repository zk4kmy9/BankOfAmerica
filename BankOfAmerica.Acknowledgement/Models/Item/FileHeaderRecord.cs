﻿using System;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// The File Header Record is the first record of the Acknowledgement 2 data file and contains nine fields. This record
    /// includes information from the input file and bank records, and the final status of the file.
    /// </summary>
    public class FileHeaderRecord
    {
        /// <summary>
        /// Customer ID (file level identifier assigned by the bank)
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Name of the input file this is acknowleding
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Creation Date/Time from the *input* File Header Record
        /// </summary>
        public DateTime FileCreationDateTime { get; set; }

        /// <summary>
        /// Resend Indicator from the *input* File Header Record
        /// </summary>
        public bool ResendIndicator { get; set; }

        /// <summary>
        /// File ID Modifier from the *input* File Header Record
        /// </summary>
        public char FileIdModifier { get; set; }

        /// <summary>
        /// Final status of File. Possible values include:
        /// "ACCEPTED-NO ADJ"
        /// "ACCEPTED-ADJ RPTD"
        /// "FILE REJECTED"
        /// </summary>
        public string FileValidationStatus { get; set; }

        public static FileHeaderRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.FileHeaderRecord)
                throw new ArgumentException();

            return new FileHeaderRecord()
            {
                CustomerId           = int.Parse(source.Substring(2, 6)),
                FileName             = source.Substring(8, 40).Trim(),
                FileCreationDateTime = DateTime.ParseExact(source.Substring(48, 12), "yyyyMMddHHmm", null),
                ResendIndicator      = source[60] == 'Y',
                FileIdModifier       = source[61],
                FileValidationStatus = source.Substring(62, 18).Trim()
            };
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            ToString(builder);
            return builder.ToString();
        }

        public void ToString(StringBuilder builder)
        {
            builder.Append(ItemRecordTypes.FileHeaderRecord);
            builder.Append(CustomerId.ToString().PadLeft(6, '0'));
            builder.Append(FileName.PadRight(40));
            builder.Append(FileCreationDateTime.ToString("yyyyMMddHHmm"));
            builder.Append(ResendIndicator ? "Y" : "N");
            builder.Append(FileIdModifier);
            builder.Append(FileValidationStatus.PadRight(18));
            builder.AppendLine();
        }
    }
}
