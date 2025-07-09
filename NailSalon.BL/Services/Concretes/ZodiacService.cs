using NailSalon.BL.Services.Abstractions;
using NailSalon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ZodiacService : IZodiacService
    {
        public string Calculate(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            return (month, day) switch
            {
                (3, >= 21) or (4, <= 19) => "Qoç",
                (4, >= 20) or (5, <= 20) => "Buğa",
                (5, >= 21) or (6, <= 20) => "Əkizlər",
                (6, >= 21) or (7, <= 22) => "Xərçəng",
                (7, >= 23) or (8, <= 22) => "Şir",
                (8, >= 23) or (9, <= 22) => "Qız",
                (9, >= 23) or (10, <= 22) => "Tərəzi",
                (10, >= 23) or (11, <= 21) => "Əqrəb",
                (11, >= 22) or (12, <= 21) => "Oxatan",
                (12, >= 22) or (1, <= 19) => "Oğlaq",
                (1, >= 20) or (2, <= 18) => "Dolça",
                (2, >= 19) or (3, <= 20) => "Balıq",
                _ => "Naməlum"
            };
        }

        public ZodiacInfo GetZodiacInfo(DateTime birthDate)
        {
            var signs = new List<ZodiacInfo>
        {
            new() { Name = "Qoç", Symbol = "♈", Trait = "Enerjili və lider ruhlu", SuggestedDesign = "Al qırmızı french" },
            new() { Name = "Buğa", Symbol = "♉", Trait = "Səbirli və zərif", SuggestedDesign = "Təbii nude tonlar" },
            new() { Name = "Əkizlər", Symbol = "♊", Trait = "Aktiv və dəyişkən", SuggestedDesign = "Rəngli blok manikür" },
            new() { Name = "Xərçəng", Symbol = "♋", Trait = "Emosional və incə", SuggestedDesign = "Çəhrayı və parıltılı dizayn" },
            new() { Name = "Şir", Symbol = "♌", Trait = "Qürurlu və cazibədar", SuggestedDesign = "Qızılı metalik dizayn" },
            new() { Name = "Qız", Symbol = "♍", Trait = "Dəqiq və təmizkar", SuggestedDesign = "Sadə fransız manikür" },
            new() { Name = "Tərəzi", Symbol = "♎", Trait = "Estetik və balanslı", SuggestedDesign = "Qrafik xətt dizaynı" },
            new() { Name = "Əqrəb", Symbol = "♏", Trait = "İntuisiyası güclü və sadiq", SuggestedDesign = "Tünd bənövşəyi dırnaqlar" },
            new() { Name = "Oxatan", Symbol = "♐", Trait = "Sərbəst və optimist", SuggestedDesign = "Dağ motivləri" },
            new() { Name = "Oğlaq", Symbol = "♑", Trait = "Ciddi və məqsədli", SuggestedDesign = "Qara-ağ minimal dizayn" },
            new() { Name = "Dolça", Symbol = "♒", Trait = "Yenilikçi və ağıllı", SuggestedDesign = "Holografik dizayn" },
            new() { Name = "Balıq", Symbol = "♓", Trait = "Emosional və yaradıcı", SuggestedDesign = "Mavi glitter gel" }
        };

            string zodiacName = Calculate(birthDate);
            return signs.FirstOrDefault(s => s.Name == zodiacName) ?? new ZodiacInfo
            {
                Name = "Naməlum",
                Symbol = "?",
                Trait = "Məlumat yoxdur",
                SuggestedDesign = "Standart dizayn"
            };
        }
    }
}
