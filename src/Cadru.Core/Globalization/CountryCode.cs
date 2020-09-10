//------------------------------------------------------------------------------
// <copyright file="CountryCode.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Cadru.Globalization
{
    /// <summary>
    /// ISO 3166 country code expressions for international aware item
    /// validation routines.
    /// </summary>
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1632:DocumentationTextMustMeetMinimumCharacterLength", Justification = "Reviewed.")]
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Reviewed.")]
    public enum CountryCode
    {
        /// <summary>
        /// No country specified
        /// </summary>
        None = 0,

        /// <summary>
        /// Invariant country specified (same as None)
        /// </summary>
        Invariant = None,

        /// <summary>
        /// Afghanistan
        /// </summary>
        [EnumDescription("Afghanistan")]
        [Display(Name = "Afghanistan")]
        [UIHint("UI")]
        AF = 004,

        /// <summary>
        /// Åland Islands
        /// </summary>
        [EnumDescription("Åland Islands")]
        [Display(Name = "Åland Islands")]
        [UIHint("UI")]
        AX = 248,

        /// <summary>
        /// Albania
        /// </summary>
        [EnumDescription("Albania")]
        [Display(Name = "Albania")]
        [UIHint("UI")]
        AL = 008,

        /// <summary>
        /// Algeria
        /// </summary>
        [EnumDescription("Algeria")]
        [Display(Name = "Algeria")]
        [UIHint("UI")]
        DZ = 012,

        /// <summary>
        /// American Samoa
        /// </summary>
        [EnumDescription("American Samoa")]
        [Display(Name = "American Samoa")]
        [UIHint("UI")]
        AS = 016,

        /// <summary>
        /// Andorra
        /// </summary>
        [EnumDescription("Andorra")]
        [Display(Name = "Andorra")]
        [UIHint("UI")]
        AD = 020,

        /// <summary>
        /// Angola
        /// </summary>
        [EnumDescription("Angola")]
        [Display(Name = "Angola")]
        [UIHint("UI")]
        AO = 024,

        /// <summary>
        /// Anguilla
        /// </summary>
        [EnumDescription("Anguilla")]
        [Display(Name = "Anguilla")]
        [UIHint("UI")]
        AI = 660,

        /// <summary>
        /// Antarctica
        /// </summary>
        [EnumDescription("Antarctica")]
        [Display(Name = "Antarctica")]
        [UIHint("UI")]
        AQ = 010,

        /// <summary>
        /// Antigua And Barbuda
        /// </summary>
        [EnumDescription("Antigua And Barbuda")]
        [Display(Name = "Antigua And Barbuda")]
        [UIHint("UI")]
        AG = 028,

        /// <summary>
        /// Argentina
        /// </summary>
        [EnumDescription("Argentina")]
        [Display(Name = "Argentina")]
        [UIHint("UI")]
        AR = 032,

        /// <summary>
        /// Armenia
        /// </summary>
        [EnumDescription("Armenia")]
        [Display(Name = "Armenia")]
        [UIHint("UI")]
        AM = 051,

        /// <summary>
        /// Aruba
        /// </summary>
        [EnumDescription("Aruba")]
        [Display(Name = "Aruba")]
        [UIHint("UI")]
        AW = 533,

        /// <summary>
        /// Australia
        /// </summary>
        [EnumDescription("Australia")]
        [Display(Name = "Australia")]
        [UIHint("UI")]
        AU = 036,

        /// <summary>
        /// Austria
        /// </summary>
        [EnumDescription("Austria")]
        [Display(Name = "Austria")]
        [UIHint("UI")]
        AT = 040,

        /// <summary>
        /// Azerbaijan
        /// </summary>
        [EnumDescription("Azerbaijan")]
        [Display(Name = "Azerbaijan")]
        [UIHint("UI")]
        AZ = 031,

        /// <summary>
        /// Bahamas
        /// </summary>
        [EnumDescription("Bahamas")]
        [Display(Name = "Bahamas")]
        [UIHint("UI")]
        BS = 044,

        /// <summary>
        /// Bahrain
        /// </summary>
        [EnumDescription("Bahrain")]
        [Display(Name = "Bahrain")]
        [UIHint("UI")]
        BH = 048,

        /// <summary>
        /// Bangladesh
        /// </summary>
        [EnumDescription("Bangladesh")]
        [Display(Name = "Bangladesh")]
        [UIHint("UI")]
        BD = 050,

        /// <summary>
        /// Barbados
        /// </summary>
        [EnumDescription("Barbados")]
        [Display(Name = "Barbados")]
        [UIHint("UI")]
        BB = 052,

        /// <summary>
        /// Belarus
        /// </summary>
        [EnumDescription("Belarus")]
        [Display(Name = "Belarus")]
        [UIHint("UI")]
        BY = 112,

        /// <summary>
        /// Belgium
        /// </summary>
        [EnumDescription("Belgium")]
        [Display(Name = "Belgium")]
        [UIHint("UI")]
        BE = 056,

        /// <summary>
        /// Belize
        /// </summary>
        [EnumDescription("Belize")]
        [Display(Name = "Belize")]
        [UIHint("UI")]
        BZ = 084,

        /// <summary>
        /// Benin
        /// </summary>
        [EnumDescription("Benin")]
        [Display(Name = "Benin")]
        [UIHint("UI")]
        BJ = 204,

        /// <summary>
        /// Bermuda
        /// </summary>
        [EnumDescription("Bermuda")]
        [Display(Name = "Bermuda")]
        [UIHint("UI")]
        BM = 060,

        /// <summary>
        /// Bhutan
        /// </summary>
        [EnumDescription("Bhutan")]
        [Display(Name = "Bhutan")]
        [UIHint("UI")]
        BT = 064,

        /// <summary>
        /// Bolivia
        /// </summary>
        [EnumDescription("Bolivia")]
        [Display(Name = "Bolivia")]
        [UIHint("UI")]
        BO = 068,

        /// <summary>
        /// Bosnia And Herzegovina
        /// </summary>
        [EnumDescription("Bosnia And Herzegovina")]
        [Display(Name = "Bosnia And Herzegovina")]
        [UIHint("UI")]
        BA = 070,

        /// <summary>
        /// Botswana
        /// </summary>
        [EnumDescription("Botswana")]
        [Display(Name = "Botswana")]
        [UIHint("UI")]
        BW = 072,

        /// <summary>
        /// Bouvet Island
        /// </summary>
        [EnumDescription("Bouvet Island")]
        [Display(Name = "Bouvet Island")]
        [UIHint("UI")]
        BV = 074,

        /// <summary>
        /// Brazil
        /// </summary>
        [EnumDescription("Brazil")]
        [Display(Name = "Brazil")]
        [UIHint("UI")]
        BR = 076,

        /// <summary>
        /// British Indian Ocean Territory
        /// </summary>
        [EnumDescription("British Indian Ocean Territory")]
        [Display(Name = "British Indian Ocean Territory")]
        [UIHint("UI")]
        IO = 086,

        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        [EnumDescription("Brunei Darussalam")]
        [Display(Name = "Brunei Darussalam")]
        [UIHint("UI")]
        BN = 096,

        /// <summary>
        /// Bulgaria
        /// </summary>
        [EnumDescription("Bulgaria")]
        [Display(Name = "Bulgaria")]
        [UIHint("UI")]
        BG = 100,

        /// <summary>
        /// Burkina Faso
        /// </summary>
        [EnumDescription("Burkina Faso")]
        [Display(Name = "Burkina Faso")]
        [UIHint("UI")]
        BF = 854,

        /// <summary>
        /// Burundi
        /// </summary>
        [EnumDescription("Burundi")]
        [Display(Name = "Burundi")]
        [UIHint("UI")]
        BI = 108,

        /// <summary>
        /// Cambodia
        /// </summary>
        [EnumDescription("Cambodia")]
        [Display(Name = "Cambodia")]
        [UIHint("UI")]
        KH = 116,

        /// <summary>
        /// Cameroon
        /// </summary>
        [EnumDescription("Cameroon")]
        [Display(Name = "Cameroon")]
        [UIHint("UI")]
        CM = 120,

        /// <summary>
        /// Canada
        /// </summary>
        [EnumDescription("Canada")]
        [Display(Name = "Canada")]
        [UIHint("UI")]
        CA = 124,

        /// <summary>
        /// Cape Verde
        /// </summary>
        [EnumDescription("Cape Verde")]
        [Display(Name = "Cape Verde")]
        [UIHint("UI")]
        CV = 132,

        /// <summary>
        /// Cayman Islands
        /// </summary>
        [EnumDescription("Cayman Islands")]
        [Display(Name = "Cayman Islands")]
        [UIHint("UI")]
        KY = 136,

        /// <summary>
        /// Central African Republic
        /// </summary>
        [EnumDescription("Central African Republic")]
        [Display(Name = "Central African Republic")]
        [UIHint("UI")]
        CF = 140,

        /// <summary>
        /// Chad
        /// </summary>
        [EnumDescription("Chad")]
        [Display(Name = "Chad")]
        [UIHint("UI")]
        TD = 148,

        /// <summary>
        /// Chile
        /// </summary>
        [EnumDescription("Chile")]
        [Display(Name = "Chile")]
        [UIHint("UI")]
        CL = 152,

        /// <summary>
        /// China
        /// </summary>
        [EnumDescription("China")]
        [Display(Name = "China")]
        [UIHint("UI")]
        CN = 156,

        /// <summary>
        /// Christmas Island
        /// </summary>
        [EnumDescription("Christmas Island")]
        [Display(Name = "Christmas Island")]
        [UIHint("UI")]
        CX = 162,

        /// <summary>
        /// Cocos (Keeling) Islands
        /// </summary>
        [EnumDescription("Cocos (Keeling) Islands")]
        [Display(Name = "Cocos (Keeling) Islands")]
        [UIHint("UI")]
        CC = 166,

        /// <summary>
        /// Colombia
        /// </summary>
        [EnumDescription("Colombia")]
        [Display(Name = "Colombia")]
        [UIHint("UI")]
        CO = 170,

        /// <summary>
        /// Comoros
        /// </summary>
        [EnumDescription("Comoros")]
        [Display(Name = "Comoros")]
        [UIHint("UI")]
        KM = 174,

        /// <summary>
        /// Congo
        /// </summary>
        [EnumDescription("Congo")]
        [Display(Name = "Congo")]
        [UIHint("UI")]
        CG = 178,

        /// <summary>
        /// Congo, The Democratic Republic Of The
        /// </summary>
        [EnumDescription("The Democratic Republic Of The Congo")]
        [Display(Name = "The Democratic Republic Of The Congo")]
        [UIHint("UI")]
        CD = 180,

        /// <summary>
        /// Cook Islands
        /// </summary>
        [EnumDescription("Cook Islands")]
        [Display(Name = "Cook Islands")]
        [UIHint("UI")]
        CK = 184,

        /// <summary>
        /// Costa Rica
        /// </summary>
        [EnumDescription("Costa Rica")]
        [Display(Name = "Costa Rica")]
        [UIHint("UI")]
        CR = 188,

        /// <summary>
        /// Cote D'Ivoire
        /// </summary>
        [EnumDescription("Cote D'Ivoire")]
        [Display(Name = "Cote D'Ivoire")]
        [UIHint("UI")]
        CI = 384,

        /// <summary>
        /// Croatia
        /// </summary>
        [EnumDescription("Croatia")]
        [Display(Name = "Croatia")]
        [UIHint("UI")]
        HR = 191,

        /// <summary>
        /// Cuba
        /// </summary>
        [EnumDescription("Cuba")]
        [Display(Name = "Cuba")]
        [UIHint("UI")]
        CU = 192,

        /// <summary>
        /// Cyprus
        /// </summary>
        [EnumDescription("Cyprus")]
        [Display(Name = "Cyprus")]
        [UIHint("UI")]
        CY = 196,

        /// <summary>
        /// Czech Republic
        /// </summary>
        [EnumDescription("Czech Republic")]
        [Display(Name = "Czech Republic")]
        [UIHint("UI")]
        CZ = 203,

        /// <summary>
        /// Denmark
        /// </summary>
        [EnumDescription("Denmark")]
        [Display(Name = "Denmark")]
        [UIHint("UI")]
        DK = 208,

        /// <summary>
        /// Djibouti
        /// </summary>
        [EnumDescription("Djibouti")]
        [Display(Name = "Djibouti")]
        [UIHint("UI")]
        DJ = 262,

        /// <summary>
        /// Dominica
        /// </summary>
        [EnumDescription("Dominica")]
        [Display(Name = "Dominica")]
        [UIHint("UI")]
        DM = 212,

        /// <summary>
        /// Dominican Republic
        /// </summary>
        [EnumDescription("Dominican Republic")]
        [Display(Name = "Dominican Republic")]
        [UIHint("UI")]
        DO = 214,

        /// <summary>
        /// Ecuador
        /// </summary>
        [EnumDescription("Ecuador")]
        [Display(Name = "Ecuador")]
        [UIHint("UI")]
        EC = 218,

        /// <summary>
        /// Egypt
        /// </summary>
        [EnumDescription("Egypt")]
        [Display(Name = "Egypt")]
        [UIHint("UI")]
        EG = 818,

        /// <summary>
        /// El Salvador
        /// </summary>
        [EnumDescription("El Salvador")]
        [Display(Name = "El Salvador")]
        [UIHint("UI")]
        SV = 222,

        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        [EnumDescription("Equatorial Guinea")]
        [Display(Name = "Equatorial Guinea")]
        [UIHint("UI")]
        GQ = 226,

        /// <summary>
        /// Eritrea
        /// </summary>
        [EnumDescription("Eritrea")]
        [Display(Name = "Eritrea")]
        [UIHint("UI")]
        ER = 232,

        /// <summary>
        /// Estonia
        /// </summary>
        [EnumDescription("Estonia")]
        [Display(Name = "Estonia")]
        [UIHint("UI")]
        EE = 233,

        /// <summary>
        /// Ethiopia
        /// </summary>
        [EnumDescription("Ethiopia")]
        [Display(Name = "Ethiopia")]
        [UIHint("UI")]
        ET = 231,

        /// <summary>
        /// Falkland Islands (Malvinas)
        /// </summary>
        [EnumDescription("Falkland Islands (Malvinas)")]
        [Display(Name = "Falkland Islands (Malvinas)")]
        [UIHint("UI")]
        FK = 238,

        /// <summary>
        /// Faroe Islands
        /// </summary>
        [EnumDescription("Faroe Islands")]
        [Display(Name = "Faroe Islands")]
        [UIHint("UI")]
        FO = 234,

        /// <summary>
        /// Fiji
        /// </summary>
        [EnumDescription("Fiji")]
        [Display(Name = "Fiji")]
        [UIHint("UI")]
        FJ = 242,

        /// <summary>
        /// Finland
        /// </summary>
        [EnumDescription("Finland")]
        [Display(Name = "Finland")]
        [UIHint("UI")]
        FI = 246,

        /// <summary>
        /// France
        /// </summary>
        [EnumDescription("France")]
        [Display(Name = "France")]
        [UIHint("UI")]
        FR = 250,

        /// <summary>
        /// French Guiana
        /// </summary>
        [EnumDescription("French Guiana")]
        [Display(Name = "French Guiana")]
        [UIHint("UI")]
        GF = 254,

        /// <summary>
        /// French Polynesia
        /// </summary>
        [EnumDescription("French Polynesia")]
        [Display(Name = "French Polynesia")]
        [UIHint("UI")]
        PF = 258,

        /// <summary>
        /// French Southern Territories
        /// </summary>
        [EnumDescription("French Southern Territories")]
        [Display(Name = "French Southern Territories")]
        [UIHint("UI")]
        TF = 260,

        /// <summary>
        /// Gabon
        /// </summary>
        [EnumDescription("Gabon")]
        [Display(Name = "Gabon")]
        [UIHint("UI")]
        GA = 266,

        /// <summary>
        /// Gambia
        /// </summary>
        [EnumDescription("Gambia")]
        [Display(Name = "Gambia")]
        [UIHint("UI")]
        GM = 270,

        /// <summary>
        /// Georgia
        /// </summary>
        [EnumDescription("Georgia")]
        [Display(Name = "Georgia")]
        [UIHint("UI")]
        GE = 268,

        /// <summary>
        /// Germany
        /// </summary>
        [EnumDescription("Germany")]
        [Display(Name = "Germany")]
        [UIHint("UI")]
        DE = 276,

        /// <summary>
        /// Ghana
        /// </summary>
        [EnumDescription("Ghana")]
        [Display(Name = "Ghana")]
        [UIHint("UI")]
        GH = 288,

        /// <summary>
        /// Gibraltar
        /// </summary>
        [EnumDescription("Gibraltar")]
        [Display(Name = "Gibraltar")]
        [UIHint("UI")]
        GI = 292,

        /// <summary>
        /// Greece
        /// </summary>
        [EnumDescription("Greece")]
        [Display(Name = "Greece")]
        [UIHint("UI")]
        GR = 300,

        /// <summary>
        /// Greenland
        /// </summary>
        [EnumDescription("Greenland")]
        [Display(Name = "Greenland")]
        [UIHint("UI")]
        GL = 304,

        /// <summary>
        /// Grenada
        /// </summary>
        [EnumDescription("Grenada")]
        [Display(Name = "Grenada")]
        [UIHint("UI")]
        GD = 308,

        /// <summary>
        /// Guadeloupe
        /// </summary>
        [EnumDescription("Guadeloupe")]
        [Display(Name = "Guadeloupe")]
        [UIHint("UI")]
        GP = 312,

        /// <summary>
        /// Guam
        /// </summary>
        [EnumDescription("Guam")]
        [Display(Name = "Guam")]
        [UIHint("UI")]
        GU = 316,

        /// <summary>
        /// Guatemala
        /// </summary>
        [EnumDescription("Guatemala")]
        [Display(Name = "Guatemala")]
        [UIHint("UI")]
        GT = 320,

        /// <summary>
        /// Guinea
        /// </summary>
        [EnumDescription("Guinea")]
        [Display(Name = "Guinea")]
        [UIHint("UI")]
        GN = 324,

        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        [EnumDescription("Guinea-Bissau")]
        [Display(Name = "Guinea-Bissau")]
        [UIHint("UI")]
        GW = 624,

        /// <summary>
        /// Guyana
        /// </summary>
        [EnumDescription("Guyana")]
        [Display(Name = "Guyana")]
        [UIHint("UI")]
        GY = 328,

        /// <summary>
        /// Haiti
        /// </summary>
        [EnumDescription("Haiti")]
        [Display(Name = "Haiti")]
        [UIHint("UI")]
        HT = 332,

        /// <summary>
        /// Heard Island And McDonald Islands
        /// </summary>
        [EnumDescription("Heard Island And McDonald Islands")]
        [Display(Name = "Heard Island And McDonald Islands")]
        [UIHint("UI")]
        HM = 334,

        /// <summary>
        /// Holy See (Vatican City State)
        /// </summary>
        [EnumDescription("Holy See (Vatican City State)")]
        [Display(Name = "Holy See (Vatican City State)")]
        [UIHint("UI")]
        VA = 336,

        /// <summary>
        /// Honduras
        /// </summary>
        [EnumDescription("Honduras")]
        [Display(Name = "Honduras")]
        [UIHint("UI")]
        HN = 340,

        /// <summary>
        /// Hong Kong
        /// </summary>
        [EnumDescription("Hong Kong")]
        [Display(Name = "Hong Kong")]
        [UIHint("UI")]
        HK = 344,

        /// <summary>
        /// Hungary
        /// </summary>
        [EnumDescription("Hungary")]
        [Display(Name = "Hungary")]
        [UIHint("UI")]
        HU = 348,

        /// <summary>
        /// Iceland
        /// </summary>
        [EnumDescription("Iceland")]
        [Display(Name = "Iceland")]
        [UIHint("UI")]
        IS = 352,

        /// <summary>
        /// India
        /// </summary>
        [EnumDescription("India")]
        [Display(Name = "India")]
        [UIHint("UI")]
        IN = 356,

        /// <summary>
        /// Indonesia
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID", Justification = "Reviewed.")]
        [EnumDescription("Indonesia")]
        [Display(Name = "Indonesia")]
        [UIHint("UI")]
        ID = 360,

        /// <summary>
        /// Iran, Islamic Republic Of
        /// </summary>
        [EnumDescription("Islamic Republic Of Iran")]
        [Display(Name = "Islamic Republic Of Iran")]
        [UIHint("UI")]
        IR = 364,

        /// <summary>
        /// Iraq
        /// </summary>
        [EnumDescription("Iraq")]
        [Display(Name = "Iraq")]
        [UIHint("UI")]
        IQ = 368,

        /// <summary>
        /// Ireland
        /// </summary>
        [EnumDescription("Ireland")]
        [Display(Name = "Ireland")]
        [UIHint("UI")]
        IE = 372,

        /// <summary>
        /// Israel
        /// </summary>
        [EnumDescription("Israel")]
        [Display(Name = "Israel")]
        [UIHint("UI")]
        IL = 376,

        /// <summary>
        /// Italy
        /// </summary>
        [EnumDescription("Italy")]
        [Display(Name = "Italy")]
        [UIHint("UI")]
        IT = 380,

        /// <summary>
        /// Jamaica
        /// </summary>
        [EnumDescription("Jamaica")]
        [Display(Name = "Jamaica")]
        [UIHint("UI")]
        JM = 388,

        /// <summary>
        /// Japan
        /// </summary>
        [EnumDescription("Afghanistan")]
        [Display(Name = "Afghanistan")]
        [UIHint("UI")]
        JP = 392,

        /// <summary>
        /// Jordan
        /// </summary>
        [EnumDescription("Jordan")]
        [Display(Name = "Jordan")]
        [UIHint("UI")]
        JO = 400,

        /// <summary>
        /// Kazakhstan
        /// </summary>
        [EnumDescription("Kazakhstan")]
        [Display(Name = "Kazakhstan")]
        [UIHint("UI")]
        KZ = 398,

        /// <summary>
        /// Kenya
        /// </summary>
        [EnumDescription("Kenya")]
        [Display(Name = "Kenya")]
        [UIHint("UI")]
        KE = 404,

        /// <summary>
        /// Kiribati
        /// </summary>
        [EnumDescription("Kiribati")]
        [Display(Name = "Kiribati")]
        [UIHint("UI")]
        KI = 296,

        /// <summary>
        /// Korea, Democratic People's Republic Of
        /// </summary>
        [EnumDescription("Democratic People's Republic Of Korea")]
        [Display(Name = "Democratic People's Republic Of Korea")]
        [UIHint("UI")]
        KP = 408,

        /// <summary>
        /// Korea, Republic Of
        /// </summary>
        [EnumDescription("Republic Of Korea")]
        [Display(Name = "Republic Of Korea")]
        [UIHint("UI")]
        KR = 410,

        /// <summary>
        /// Kuwait
        /// </summary>
        [EnumDescription("Kuwait")]
        [Display(Name = "Kuwait")]
        [UIHint("UI")]
        KW = 414,

        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        [EnumDescription("Kyrgyzstan")]
        [Display(Name = "Kyrgyzstan")]
        [UIHint("UI")]
        KG = 417,

        /// <summary>
        /// Lao People's Democratic Republic
        /// </summary>
        [EnumDescription("Lao People's Democratic Republic")]
        [Display(Name = "Lao People's Democratic Republic")]
        [UIHint("UI")]
        LA = 418,

        /// <summary>
        /// Latvia
        /// </summary>
        [EnumDescription("Latvia")]
        [Display(Name = "Latvia")]
        [UIHint("UI")]
        LV = 428,

        /// <summary>
        /// Lebanon
        /// </summary>
        [EnumDescription("Lebanon")]
        [Display(Name = "Lebanon")]
        [UIHint("UI")]
        LB = 422,

        /// <summary>
        /// Lesotho
        /// </summary>
        [EnumDescription("Lesotho")]
        [Display(Name = "Lesotho")]
        [UIHint("UI")]
        LS = 426,

        /// <summary>
        /// Liberia
        /// </summary>
        [EnumDescription("Liberia")]
        [Display(Name = "Liberia")]
        [UIHint("UI")]
        LR = 430,

        /// <summary>
        /// Libyan Arab Jamahiriya
        /// </summary>
        [EnumDescription("Libyan Arab Jamahiriya")]
        [Display(Name = "Libyan Arab Jamahiriya")]
        [UIHint("UI")]
        LY = 434,

        /// <summary>
        /// Liechtenstein
        /// </summary>
        [EnumDescription("Liechtenstein")]
        [Display(Name = "Liechtenstein")]
        [UIHint("UI")]
        LI = 438,

        /// <summary>
        /// Lithuania
        /// </summary>
        [EnumDescription("Lithuania")]
        [Display(Name = "Lithuania")]
        [UIHint("UI")]
        LT = 440,

        /// <summary>
        /// Luxembourg
        /// </summary>
        [EnumDescription("Luxembourg")]
        [Display(Name = "Luxembourg")]
        [UIHint("UI")]
        LU = 442,

        /// <summary>
        /// Macao
        /// </summary>
        [EnumDescription("Macao")]
        [Display(Name = "Macao")]
        [UIHint("UI")]
        MO = 446,

        /// <summary>
        /// Macedonia, The Former Yugoslav Republic Of
        /// </summary>
        [EnumDescription("The Former Yugoslav Republic Of Macedonia")]
        [Display(Name = "The Former Yugoslav Republic Of Macedonia")]
        [UIHint("UI")]
        MK = 807,

        /// <summary>
        /// Madagascar
        /// </summary>
        [EnumDescription("Madagascar")]
        [Display(Name = "Madagascar")]
        [UIHint("UI")]
        MG = 450,

        /// <summary>
        /// Malawi
        /// </summary>
        [EnumDescription("Malawi")]
        [Display(Name = "Malawi")]
        [UIHint("UI")]
        MW = 454,

        /// <summary>
        /// Malaysia
        /// </summary>
        [EnumDescription("Malaysia")]
        [Display(Name = "Malaysia")]
        [UIHint("UI")]
        MY = 458,

        /// <summary>
        /// Maldives
        /// </summary>
        [EnumDescription("Maldives")]
        [Display(Name = "Maldives")]
        [UIHint("UI")]
        MV = 462,

        /// <summary>
        /// Mali
        /// </summary>
        [EnumDescription("Mali")]
        [Display(Name = "Mali")]
        [UIHint("UI")]
        ML = 466,

        /// <summary>
        /// Malta
        /// </summary>
        [EnumDescription("Malta")]
        [Display(Name = "Malta")]
        [UIHint("UI")]
        MT = 470,

        /// <summary>
        /// Marshall Islands
        /// </summary>
        [EnumDescription("Marshall Islands")]
        [Display(Name = "Marshall Islands")]
        [UIHint("UI")]
        MH = 584,

        /// <summary>
        /// Martinique
        /// </summary>
        [EnumDescription("Martinique")]
        [Display(Name = "Martinique")]
        [UIHint("UI")]
        MQ = 474,

        /// <summary>
        /// Mauritania
        /// </summary>
        [EnumDescription("Mauritania")]
        [Display(Name = "Mauritania")]
        [UIHint("UI")]
        MR = 478,

        /// <summary>
        /// Mauritius
        /// </summary>
        [EnumDescription("Mauritius")]
        [Display(Name = "Mauritius")]
        [UIHint("UI")]
        MU = 480,

        /// <summary>
        /// Mayotte
        /// </summary>
        [EnumDescription("Mayotte")]
        [Display(Name = "Mayotte")]
        [UIHint("UI")]
        YT = 175,

        /// <summary>
        /// Mexico
        /// </summary>
        [EnumDescription("Mexico")]
        [Display(Name = "Mexico")]
        [UIHint("UI")]
        MX = 484,

        /// <summary>
        /// Micronesia, Federated States Of
        /// </summary>
        [EnumDescription("Federated States Of Micronesia")]
        [Display(Name = "Federated States Of Micronesia")]
        [UIHint("UI")]
        FM = 583,

        /// <summary>
        /// Moldova, Republic Of
        /// </summary>
        [EnumDescription("Republic Of Moldova")]
        [Display(Name = "Republic Of Moldova")]
        [UIHint("UI")]
        MD = 498,

        /// <summary>
        /// Monaco
        /// </summary>
        [EnumDescription("Monaco")]
        [Display(Name = "Monaco")]
        [UIHint("UI")]
        MC = 492,

        /// <summary>
        /// Mongolia
        /// </summary>
        [EnumDescription("Mongolia")]
        [Display(Name = "Mongolia")]
        [UIHint("UI")]
        MN = 496,

        /// <summary>
        /// Montserrat
        /// </summary>
        [EnumDescription("Montserrat")]
        [Display(Name = "Montserrat")]
        [UIHint("UI")]
        MS = 500,

        /// <summary>
        /// Morocco
        /// </summary>
        [EnumDescription("Morocco")]
        [Display(Name = "Morocco")]
        [UIHint("UI")]
        MA = 504,

        /// <summary>
        /// Mozambique
        /// </summary>
        [EnumDescription("Mozambique")]
        [Display(Name = "Mozambique")]
        [UIHint("UI")]
        MZ = 508,

        /// <summary>
        /// Myanmar
        /// </summary>
        [EnumDescription("Myanmar")]
        [Display(Name = "Myanmar")]
        [UIHint("UI")]
        MM = 104,

        /// <summary>
        /// Namibia
        /// </summary>
        [EnumDescription("Namibia")]
        [Display(Name = "Namibia")]
        [UIHint("UI")]
        NA = 516,

        /// <summary>
        /// Nauru
        /// </summary>
        [EnumDescription("Nauru")]
        [Display(Name = "Nauru")]
        [UIHint("UI")]
        NR = 520,

        /// <summary>
        /// Nepal
        /// </summary>
        [EnumDescription("Nepal")]
        [Display(Name = "Nepal")]
        [UIHint("UI")]
        NP = 524,

        /// <summary>
        /// Netherlands
        /// </summary>
        [EnumDescription("Netherlands")]
        [Display(Name = "Netherlands")]
        [UIHint("UI")]
        NL = 528,

        /// <summary>
        /// Netherlands Antilles
        /// </summary>
        [EnumDescription("Netherlands Antilles")]
        [Display(Name = "Netherlands Antilles")]
        [UIHint("UI")]
        AN = 530,

        /// <summary>
        /// New Caledonia
        /// </summary>
        [EnumDescription("New Caledonia")]
        [Display(Name = "New Caledonia")]
        [UIHint("UI")]
        NC = 540,

        /// <summary>
        /// New Zealand
        /// </summary>
        [EnumDescription("New Zealand")]
        [Display(Name = "New Zealand")]
        [UIHint("UI")]
        NZ = 554,

        /// <summary>
        /// Nicaragua
        /// </summary>
        [EnumDescription("Nicaragua")]
        [Display(Name = "Nicaragua")]
        [UIHint("UI")]
        NI = 558,

        /// <summary>
        /// Niger
        /// </summary>
        [EnumDescription("Niger")]
        [Display(Name = "Niger")]
        [UIHint("UI")]
        NE = 562,

        /// <summary>
        /// Nigeria
        /// </summary>
        [EnumDescription("Nigeria")]
        [Display(Name = "Nigeria")]
        [UIHint("UI")]
        NG = 566,

        /// <summary>
        /// Niue
        /// </summary>
        [EnumDescription("Niue")]
        [Display(Name = "Niue")]
        [UIHint("UI")]
        NU = 570,

        /// <summary>
        /// Norfolk Island
        /// </summary>
        [EnumDescription("Norfolk Island")]
        [Display(Name = "Norfolk Island")]
        [UIHint("UI")]
        NF = 574,

        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        [EnumDescription("Northern Mariana Islands")]
        [Display(Name = "Northern Mariana Islands")]
        [UIHint("UI")]
        MP = 580,

        /// <summary>
        /// Norway
        /// </summary>
        [EnumDescription("Norway")]
        [Display(Name = "Norway")]
        [UIHint("UI")]
        NO = 578,

        /// <summary>
        /// Oman
        /// </summary>
        [EnumDescription("Oman")]
        [Display(Name = "Oman")]
        [UIHint("UI")]
        OM = 512,

        /// <summary>
        /// Pakistan
        /// </summary>
        [EnumDescription("Pakistan")]
        [Display(Name = "Pakistan")]
        [UIHint("UI")]
        PK = 586,

        /// <summary>
        /// Palau
        /// </summary>
        [EnumDescription("Palau")]
        [Display(Name = "Palau")]
        [UIHint("UI")]
        PW = 585,

        /// <summary>
        /// Palestinian Territory, Occupied
        /// </summary>
        [EnumDescription("Occupied Palestinian Territory")]
        [Display(Name = "Occupied Palestinian Territory")]
        [UIHint("UI")]
        PS = 275,

        /// <summary>
        /// Panama
        /// </summary>
        [EnumDescription("Panama")]
        [Display(Name = "Panama")]
        [UIHint("UI")]
        PA = 591,

        /// <summary>
        /// Papua New Guinea
        /// </summary>
        [EnumDescription("Papua New Guinea")]
        [Display(Name = "Papua New Guinea")]
        [UIHint("UI")]
        PG = 598,

        /// <summary>
        /// Paraguay
        /// </summary>
        [EnumDescription("Paraguay")]
        [Display(Name = "Paraguay")]
        [UIHint("UI")]
        PY = 600,

        /// <summary>
        /// Peru
        /// </summary>
        [EnumDescription("Peru")]
        [Display(Name = "Peru")]
        [UIHint("UI")]
        PE = 604,

        /// <summary>
        /// Philippines
        /// </summary>
        [EnumDescription("Philippines")]
        [Display(Name = "Philippines")]
        [UIHint("UI")]
        PH = 608,

        /// <summary>
        /// Pitcairn
        /// </summary>
        [EnumDescription("Pitcairn")]
        [Display(Name = "Pitcairn")]
        [UIHint("UI")]
        PN = 612,

        /// <summary>
        /// Poland
        /// </summary>
        [EnumDescription("Poland")]
        [Display(Name = "Poland")]
        [UIHint("UI")]
        PL = 616,

        /// <summary>
        /// Portugal
        /// </summary>
        [EnumDescription("Portugal")]
        [Display(Name = "Portugal")]
        [UIHint("UI")]
        PT = 620,

        /// <summary>
        /// Puerto Rico
        /// </summary>
        [EnumDescription("Puerto Rico")]
        [Display(Name = "Puerto Rico")]
        [UIHint("UI")]
        PR = 630,

        /// <summary>
        /// Qatar
        /// </summary>
        [EnumDescription("Qatar")]
        [Display(Name = "Qatar")]
        [UIHint("UI")]
        QA = 634,

        /// <summary>
        /// Reunion
        /// </summary>
        [EnumDescription("Reunion")]
        [Display(Name = "Reunion")]
        [UIHint("UI")]
        RE = 638,

        /// <summary>
        /// Romania
        /// </summary>
        [EnumDescription("Romania")]
        [Display(Name = "Romania")]
        [UIHint("UI")]
        RO = 642,

        /// <summary>
        /// Russian Federation
        /// </summary>
        [EnumDescription("Russian Federation")]
        [Display(Name = "Russian Federation")]
        [UIHint("UI")]
        RU = 643,

        /// <summary>
        /// Rwanda
        /// </summary>
        [EnumDescription("Rwanda")]
        [Display(Name = "Rwanda")]
        [UIHint("UI")]
        RW = 646,

        /// <summary>
        /// Saint Helena
        /// </summary>
        [EnumDescription("Saint Helena")]
        [Display(Name = "Saint Helena")]
        [UIHint("UI")]
        SH = 654,

        /// <summary>
        /// Saint Kitts And Nevis
        /// </summary>
        [EnumDescription("Saint Kitts And Nevis")]
        [Display(Name = "Saint Kitts And Nevis")]
        [UIHint("UI")]
        KN = 659,

        /// <summary>
        /// Saint Lucia
        /// </summary>
        [EnumDescription("Saint Lucia")]
        [Display(Name = "Saint Lucia")]
        [UIHint("UI")]
        LC = 662,

        /// <summary>
        /// Saint Pierre And Miquelon
        /// </summary>
        [EnumDescription("Saint Pierre And Miquelon")]
        [Display(Name = "Saint Pierre And Miquelon")]
        [UIHint("UI")]
        PM = 666,

        /// <summary>
        /// Saint Vincent And The Grenadines
        /// </summary>
        [EnumDescription("Saint Vincent And The Grenadines")]
        [Display(Name = "Saint Vincent And The Grenadines")]
        [UIHint("UI")]
        VC = 670,

        /// <summary>
        /// Samoa
        /// </summary>
        [EnumDescription("Samoa")]
        [Display(Name = "Samoa")]
        [UIHint("UI")]
        WS = 882,

        /// <summary>
        /// San Marino
        /// </summary>
        [EnumDescription("San Marino")]
        [Display(Name = "San Marino")]
        [UIHint("UI")]
        SM = 674,

        /// <summary>
        /// Sao Tome And Principe
        /// </summary>
        [EnumDescription("Sao Tome And Principe")]
        [Display(Name = "Sao Tome And Principe")]
        [UIHint("UI")]
        ST = 678,

        /// <summary>
        /// Saudi Arabia
        /// </summary>
        [EnumDescription("Afghanistan")]
        [Display(Name = "Afghanistan")]
        [UIHint("UI")]
        SA = 682,

        /// <summary>
        /// Senegal
        /// </summary>
        [EnumDescription("Senegal")]
        [Display(Name = "Senegal")]
        [UIHint("UI")]
        SN = 686,

        /// <summary>
        /// Serbia And Montenegro
        /// </summary>
        [EnumDescription("Serbia And Montenegro")]
        [Display(Name = "Serbia And Montenegro")]
        [UIHint("UI")]
        CS = 891,

        /// <summary>
        /// Seychelles
        /// </summary>
        [EnumDescription("Seychelles")]
        [Display(Name = "Seychelles")]
        [UIHint("UI")]
        SC = 690,

        /// <summary>
        /// Sierra Leone
        /// </summary>
        [EnumDescription("Sierra Leone")]
        [Display(Name = "Sierra Leone")]
        [UIHint("UI")]
        SL = 694,

        /// <summary>
        /// Singapore
        /// </summary>
        [EnumDescription("Singapore")]
        [Display(Name = "Singapore")]
        [UIHint("UI")]
        SG = 702,

        /// <summary>
        /// Slovakia
        /// </summary>
        [EnumDescription("Slovakia")]
        [Display(Name = "Slovakia")]
        [UIHint("UI")]
        SK = 703,

        /// <summary>
        /// Slovenia
        /// </summary>
        [EnumDescription("Slovenia")]
        [Display(Name = "Slovenia")]
        [UIHint("UI")]
        SI = 705,

        /// <summary>
        /// Solomon Islands
        /// </summary>
        [EnumDescription("Solomon Islands")]
        [Display(Name = "Solomon Islands")]
        [UIHint("UI")]
        SB = 090,

        /// <summary>
        /// Somalia
        /// </summary>
        [EnumDescription("Somalia")]
        [Display(Name = "Somalia")]
        [UIHint("UI")]
        SO = 706,

        /// <summary>
        /// South Africa
        /// </summary>
        [EnumDescription("South Africa")]
        [Display(Name = "South Africa")]
        [UIHint("UI")]
        ZA = 710,

        /// <summary>
        /// South Georgia And The South Sandwich Islands
        /// </summary>
        [EnumDescription("South Georgia And The South Sandwich Islands")]
        [Display(Name = "South Georgia And The South Sandwich Islands")]
        [UIHint("UI")]
        GS = 239,

        /// <summary>
        /// Spain
        /// </summary>
        [EnumDescription("Spain")]
        [Display(Name = "Spain")]
        [UIHint("UI")]
        ES = 724,

        /// <summary>
        /// Sri Lanka
        /// </summary>
        [EnumDescription("Sri Lanka")]
        [Display(Name = "Sri Lanka")]
        [UIHint("UI")]
        LK = 144,

        /// <summary>
        /// Sudan
        /// </summary>
        [EnumDescription("Sudan")]
        [Display(Name = "Sudan")]
        [UIHint("UI")]
        SD = 736,

        /// <summary>
        /// Suriname
        /// </summary>
        [EnumDescription("Suriname")]
        [Display(Name = "Suriname")]
        [UIHint("UI")]
        SR = 740,

        /// <summary>
        /// Svalbard And Jan Mayen
        /// </summary>
        [EnumDescription("Svalbard And Jan Mayen")]
        [Display(Name = "Svalbard And Jan Mayen")]
        [UIHint("UI")]
        SJ = 744,

        /// <summary>
        /// Swaziland
        /// </summary>
        [EnumDescription("Swaziland")]
        [Display(Name = "Swaziland")]
        [UIHint("UI")]
        SZ = 748,

        /// <summary>
        /// Sweden
        /// </summary>
        [EnumDescription("Afghanistan")]
        [Display(Name = "Afghanistan")]
        [UIHint("UI")]
        SE = 752,

        /// <summary>
        /// Switzerland
        /// </summary>
        [EnumDescription("Switzerland")]
        [Display(Name = "Switzerland")]
        [UIHint("UI")]
        CH = 756,

        /// <summary>
        /// Syrian Arab Republic
        /// </summary>
        [EnumDescription("Syrian Arab Republic")]
        [Display(Name = "Syrian Arab Republic")]
        [UIHint("UI")]
        SY = 760,

        /// <summary>
        /// Taiwan, Province Of China
        /// </summary>
        [EnumDescription("Taiwan")]
        [Display(Name = "Taiwan")]
        [UIHint("UI")]
        TW = 158,

        /// <summary>
        /// Tajikistan
        /// </summary>
        [EnumDescription("Tajikistan")]
        [Display(Name = "Tajikistan")]
        [UIHint("UI")]
        TJ = 762,

        /// <summary>
        /// Tanzania, United Republic Of
        /// </summary>
        [EnumDescription("United Republic Of Tanzania")]
        [Display(Name = "United Republic Of Tanzania")]
        [UIHint("UI")]
        TZ = 834,

        /// <summary>
        /// Thailand
        /// </summary>
        [EnumDescription("Thailand")]
        [Display(Name = "Thailand")]
        [UIHint("UI")]
        TH = 764,

        /// <summary>
        /// Timor-Leste
        /// </summary>
        [EnumDescription("Timor-Leste")]
        [Display(Name = "Timor-Leste")]
        [UIHint("UI")]
        TL = 626,

        /// <summary>
        /// Togo
        /// </summary>
        [EnumDescription("Togo")]
        [Display(Name = "Togo")]
        [UIHint("UI")]
        TG = 768,

        /// <summary>
        /// Tokelau
        /// </summary>
        [EnumDescription("Tokelau")]
        [Display(Name = "Tokelau")]
        [UIHint("UI")]
        TK = 772,

        /// <summary>
        /// Tonga
        /// </summary>
        [EnumDescription("Tonga")]
        [Display(Name = "Tonga")]
        [UIHint("UI")]
        TO = 776,

        /// <summary>
        /// Trinidad And Tobago
        /// </summary>
        [EnumDescription("Trinidad And Tobago")]
        [Display(Name = "Trinidad And Tobago")]
        [UIHint("UI")]
        TT = 780,

        /// <summary>
        /// Tunisia
        /// </summary>
        [EnumDescription("Tunisia")]
        [Display(Name = "Tunisia")]
        [UIHint("UI")]
        TN = 788,

        /// <summary>
        /// Turkey
        /// </summary>
        [EnumDescription("Turkey")]
        [Display(Name = "Turkey")]
        [UIHint("UI")]
        TR = 792,

        /// <summary>
        /// Turkmenistan
        /// </summary>
        [EnumDescription("Turkmenistan")]
        [Display(Name = "Turkmenistan")]
        [UIHint("UI")]
        TM = 795,

        /// <summary>
        /// Turks And Caicos Islands
        /// </summary>
        [EnumDescription("Turks And Caicos Islands")]
        [Display(Name = "Turks And Caicos Islands")]
        [UIHint("UI")]
        TC = 796,

        /// <summary>
        /// Tuvalu
        /// </summary>
        [EnumDescription("Tuvalu")]
        [Display(Name = "Tuvalu")]
        [UIHint("UI")]
        TV = 798,

        /// <summary>
        /// Uganda
        /// </summary>
        [EnumDescription("Uganda")]
        [Display(Name = "Uganda")]
        [UIHint("UI")]
        UG = 800,

        /// <summary>
        /// Ukraine
        /// </summary>
        [EnumDescription("Ukraine")]
        [Display(Name = "Ukraine")]
        [UIHint("UI")]
        UA = 804,

        /// <summary>
        /// United Arab Emirates
        /// </summary>
        [EnumDescription("United Arab Emirates")]
        [Display(Name = "United Arab Emirates")]
        [UIHint("UI")]
        AE = 784,

        /// <summary>
        /// United Kingdom
        /// </summary>
        [EnumDescription("United Kingdom")]
        [Display(Name = "United Kingdom")]
        [UIHint("UI")]
        GB = 826,

        /// <summary>
        /// United States
        /// </summary>
        [EnumDescription("United States")]
        [Display(Name = "United States")]
        [UIHint("UI")]
        US = 840,

        /// <summary>
        /// United States Minor Outlying Islands
        /// </summary>
        [EnumDescription("United States Minor Outlying Islands")]
        [Display(Name = "United States Minor Outlying Islands")]
        [UIHint("UI")]
        UM = 581,

        /// <summary>
        /// Uruguay
        /// </summary>
        [EnumDescription("Uruguay")]
        [Display(Name = "Uruguay")]
        [UIHint("UI")]
        UY = 858,

        /// <summary>
        /// Uzbekistan
        /// </summary>
        [EnumDescription("Uzbekistan")]
        [Display(Name = "Uzbekistan")]
        [UIHint("UI")]
        UZ = 860,

        /// <summary>
        /// Vanuatu
        /// </summary>
        [EnumDescription("Afghanistan")]
        [Display(Name = "Afghanistan")]
        [UIHint("UI")]
        VU = 548,

        /// <summary>
        /// Venezuela
        /// </summary>
        [EnumDescription("Venezuela")]
        [Display(Name = "Venezuela")]
        [UIHint("UI")]
        VE = 862,

        /// <summary>
        /// Viet Nam
        /// </summary>
        [EnumDescription("Viet Nam")]
        [Display(Name = "Viet Nam")]
        [UIHint("UI")]
        VN = 704,

        /// <summary>
        /// Virgin Islands, British
        /// </summary>
        [EnumDescription("British Virgin Islands")]
        [Display(Name = "British Virgin Islands")]
        [UIHint("UI")]
        VG = 092,

        /// <summary>
        /// Virgin Islands, U.S.
        /// </summary>
        [EnumDescription("U.S. Virgin Islands")]
        [Display(Name = "U.S. Virgin Islands")]
        [UIHint("UI")]
        VI = 850,

        /// <summary>
        /// Wallis And Futuna
        /// </summary>
        [EnumDescription("Wallis And Futuna")]
        [Display(Name = "Wallis And Futuna")]
        [UIHint("UI")]
        WF = 876,

        /// <summary>
        /// Western Sahara
        /// </summary>
        [EnumDescription("Western Sahara")]
        [Display(Name = "Western Sahara")]
        [UIHint("UI")]
        EH = 732,

        /// <summary>
        /// Yemen
        /// </summary>
        [EnumDescription("Yemen")]
        [Display(Name = "Yemen")]
        [UIHint("UI")]
        YE = 887,

        /// <summary>
        /// Zambia
        /// </summary>
        [EnumDescription("Zambia")]
        [Display(Name = "Zambia")]
        [UIHint("UI")]
        ZM = 894,

        /// <summary>
        /// Zimbabwe
        /// </summary>
        [EnumDescription("Zimbabwe")]
        [Display(Name = "Zimbabwe")]
        [UIHint("UI")]
        ZW = 716
    }
}