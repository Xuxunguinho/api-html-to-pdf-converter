using ChromiumHtmlToPdfLib.Enums;

namespace apiConverterHtmlToPdf.Application.DTOS
{
    public class PageSettingDto
    {

        //
        // Resumo:
        //     Paper orientation. Defaults to false.
        public bool Landscape { get; set; }

        //
        // Resumo:
        //     Top margin in inches. Defaults to 1cm (~0.4 inches).
        public double? Width { get; set; }
        public double? Height { get; set; }

        public double? MarginTop { get; set; }
        //
        // Resumo:
        //     Bottom margin in inches. Defaults to 1cm (~0.4 inches).
        public double? MarginBottom { get; set; }
        //
        // Resumo:
        //     Left margin in inches. Defaults to 1cm (~0.4 inches).
        public double? MarginLeft { get; set; }
        //
        // Resumo:
        //     Right margin in inches. Defaults to 1cm (~0.4 inches).
        public double? MarginRight { get; set; }

        /// <summary>
        ///Paper format, Support values for:
        ///
        ///0=Letter       
        ///1=Legal      
        ///2=Tabloid     
        ///3=Ledger       
        ///4=A0
        ///5=A1
        ///6=A2      
        ///7=A3     
        ///8=A4    
        ///9=A5       
        ///10=A6       
        ///11=FitPageToContent
        /// </summary>
        public PaperFormat? PaperFormat { get;  set; }


    }
}
