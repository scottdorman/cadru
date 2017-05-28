//------------------------------------------------------------------------------
// <copyright file="CountryCode.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    public enum CountryCode : int
    {
        /// <summary>No country specified</summary>
        None = 0,

        /// <summary>Invariant country specified (same as None)</summary>
        Invariant = None,

        /// <summary>Afghanistan</summary>
        AF = 004,

        /// <summary>Åland Islands</summary>
        AX = 248,

        /// <summary>Albania</summary>
        AL = 008,

        /// <summary>Algeria</summary>
        DZ = 012,

        /// <summary>American Samoa</summary>
        AS = 016,

        /// <summary>Andorra</summary>
        AD = 020,

        /// <summary>Angola</summary>
        AO = 024,

        /// <summary>Anguilla</summary>
        AI = 660,

        /// <summary>Antarctica</summary>
        AQ = 010,

        /// <summary>Antigua And Barbuda</summary>
        AG = 028,

        /// <summary>Argentina</summary>
        AR = 032,

        /// <summary>Armenia</summary>
        AM = 051,

        /// <summary>Aruba</summary>
        AW = 533,

        /// <summary>Australia</summary>
        AU = 036,

        /// <summary>Austria</summary>
        AT = 040,

        /// <summary>Azerbaijan</summary>
        AZ = 031,

        /// <summary>Bahamas</summary>
        BS = 044,

        /// <summary>Bahrain</summary>
        BH = 048,

        /// <summary>Bangladesh</summary>
        BD = 050,

        /// <summary>Barbados</summary>
        BB = 052,

        /// <summary>Belarus</summary>
        BY = 112,

        /// <summary>Belgium</summary>
        BE = 056,

        /// <summary>Belize</summary>
        BZ = 084,

        /// <summary>Benin</summary>
        BJ = 204,

        /// <summary>Bermuda</summary>
        BM = 060,

        /// <summary>Bhutan</summary>
        BT = 064,

        /// <summary>Bolivia</summary>
        BO = 068,

        /// <summary>Bosnia And Herzegovina</summary>
        BA = 070,

        /// <summary>Botswana</summary>
        BW = 072,

        /// <summary>Bouvet Island</summary>
        BV = 074,

        /// <summary>Brazil</summary>
        BR = 076,

        /// <summary>British Indian Ocean Territory</summary>
        IO = 086,

        /// <summary>Brunei Darussalam</summary>
        BN = 096,

        /// <summary>Bulgaria</summary>
        BG = 100,

        /// <summary>Burkina Faso</summary>
        BF = 854,

        /// <summary>Burundi</summary>
        BI = 108,

        /// <summary>Cambodia</summary>
        KH = 116,

        /// <summary>Cameroon</summary>
        CM = 120,

        /// <summary>Canada</summary>
        CA = 124,

        /// <summary>Cape Verde</summary>
        CV = 132,

        /// <summary>Cayman Islands</summary>
        KY = 136,

        /// <summary>Central African Republic</summary>
        CF = 140,

        /// <summary>Chad</summary>
        TD = 148,

        /// <summary>Chile</summary>
        CL = 152,

        /// <summary>China</summary>
        CN = 156,

        /// <summary>Christmas Island</summary>
        CX = 162,

        /// <summary>Cocos (Keeling) Islands</summary>
        CC = 166,

        /// <summary>Colombia</summary>
        CO = 170,

        /// <summary>Comoros</summary>
        KM = 174,

        /// <summary>Congo</summary>
        CG = 178,

        /// <summary>Congo, The Democratic Republic Of The</summary>
        CD = 180,

        /// <summary>Cook Islands</summary>
        CK = 184,

        /// <summary>Costa Rica</summary>
        CR = 188,

        /// <summary>Cote D'Ivoire</summary>
        CI = 384,

        /// <summary>Croatia</summary>
        HR = 191,

        /// <summary>Cuba</summary>
        CU = 192,

        /// <summary>Cyprus</summary>
        CY = 196,

        /// <summary>Czech Republic</summary>
        CZ = 203,

        /// <summary>Denmark</summary>
        DK = 208,

        /// <summary>Djibouti</summary>
        DJ = 262,

        /// <summary>Dominica</summary>
        DM = 212,

        /// <summary>Dominican Republic</summary>
        DO = 214,

        /// <summary>Ecuador</summary>
        EC = 218,

        /// <summary>Egypt</summary>
        EG = 818,

        /// <summary>El Salvador</summary>
        SV = 222,

        /// <summary>Equatorial Guinea</summary>
        GQ = 226,

        /// <summary>Eritrea</summary>
        ER = 232,

        /// <summary>Estonia</summary>
        EE = 233,

        /// <summary>Ethiopia</summary>
        ET = 231,

        /// <summary>Falkland Islands (Malvinas)</summary>
        FK = 238,

        /// <summary>Faroe Islands</summary>
        FO = 234,

        /// <summary>Fiji</summary>
        FJ = 242,

        /// <summary>Finland</summary>
        FI = 246,

        /// <summary>France</summary>
        FR = 250,

        /// <summary>French Guiana</summary>
        GF = 254,

        /// <summary>French Polynesia</summary>
        PF = 258,

        /// <summary>French Southern Territories</summary>
        TF = 260,

        /// <summary>Gabon</summary>
        GA = 266,

        /// <summary>Gambia</summary>
        GM = 270,

        /// <summary>Georgia</summary>
        GE = 268,

        /// <summary>Germany</summary>
        DE = 276,

        /// <summary>Ghana</summary>
        GH = 288,

        /// <summary>Gibraltar</summary>
        GI = 292,

        /// <summary>Greece</summary>
        GR = 300,

        /// <summary>Greenland</summary>
        GL = 304,

        /// <summary>Grenada</summary>
        GD = 308,

        /// <summary>Guadeloupe</summary>
        GP = 312,

        /// <summary>Guam</summary>
        GU = 316,

        /// <summary>Guatemala</summary>
        GT = 320,

        /// <summary>Guinea</summary>
        GN = 324,

        /// <summary>Guinea-Bissau</summary>
        GW = 624,

        /// <summary>Guyana</summary>
        GY = 328,

        /// <summary>Haiti</summary>
        HT = 332,

        /// <summary>Heard Island And McDonald Islands</summary>
        HM = 334,

        /// <summary>Holy See (Vatican City State)</summary>
        VA = 336,

        /// <summary>Honduras</summary>
        HN = 340,

        /// <summary>Hong Kong</summary>
        HK = 344,

        /// <summary>Hungary</summary>
        HU = 348,

        /// <summary>Iceland</summary>
        IS = 352,

        /// <summary>India</summary>
        IN = 356,

        /// <summary>Indonesia</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID", Justification = "Reviewed.")]
        ID = 360,

        /// <summary>Iran, Islamic Republic Of</summary>
        IR = 364,

        /// <summary>Iraq</summary>
        IQ = 368,

        /// <summary>Ireland</summary>
        IE = 372,

        /// <summary>Israel</summary>
        IL = 376,

        /// <summary>Italy</summary>
        IT = 380,

        /// <summary>Jamaica</summary>
        JM = 388,

        /// <summary>Japan</summary>
        JP = 392,

        /// <summary>Jordan</summary>
        JO = 400,

        /// <summary>Kazakhstan</summary>
        KZ = 398,

        /// <summary>Kenya</summary>
        KE = 404,

        /// <summary>Kiribati</summary>
        KI = 296,

        /// <summary>Korea, Democratic People'S Republic Of</summary>
        KP = 408,

        /// <summary>Korea, Republic Of</summary>
        KR = 410,

        /// <summary>Kuwait</summary>
        KW = 414,

        /// <summary>Kyrgyzstan</summary>
        KG = 417,

        /// <summary>Lao People'S Democratic Republic</summary>
        LA = 418,

        /// <summary>Latvia</summary>
        LV = 428,

        /// <summary>Lebanon</summary>
        LB = 422,

        /// <summary>Lesotho</summary>
        LS = 426,

        /// <summary>Liberia</summary>
        LR = 430,

        /// <summary>Libyan Arab Jamahiriya</summary>
        LY = 434,

        /// <summary>Liechtenstein</summary>
        LI = 438,

        /// <summary>Lithuania</summary>
        LT = 440,

        /// <summary>Luxembourg</summary>
        LU = 442,

        /// <summary>Macao</summary>
        MO = 446,

        /// <summary>Macedonia, The Former Yugoslav Republic Of</summary>
        MK = 807,

        /// <summary>Madagascar</summary>
        MG = 450,

        /// <summary>Malawi</summary>
        MW = 454,

        /// <summary>Malaysia</summary>
        MY = 458,

        /// <summary>Maldives</summary>
        MV = 462,

        /// <summary>Mali</summary>
        ML = 466,

        /// <summary>Malta</summary>
        MT = 470,

        /// <summary>Marshall Islands</summary>
        MH = 584,

        /// <summary>Martinique</summary>
        MQ = 474,

        /// <summary>Mauritania</summary>
        MR = 478,

        /// <summary>Mauritius</summary>
        MU = 480,

        /// <summary>Mayotte</summary>
        YT = 175,

        /// <summary>Mexico</summary>
        MX = 484,

        /// <summary>Micronesia, Federated States Of</summary>
        FM = 583,

        /// <summary>Moldova, Republic Of</summary>
        MD = 498,

        /// <summary>Monaco</summary>
        MC = 492,

        /// <summary>Mongolia</summary>
        MN = 496,

        /// <summary>Montserrat</summary>
        MS = 500,

        /// <summary>Morocco</summary>
        MA = 504,

        /// <summary>Mozambique</summary>
        MZ = 508,

        /// <summary>Myanmar</summary>
        MM = 104,

        /// <summary>Namibia</summary>
        NA = 516,

        /// <summary>Nauru</summary>
        NR = 520,

        /// <summary>Nepal</summary>
        NP = 524,

        /// <summary>Netherlands</summary>
        NL = 528,

        /// <summary>Netherlands Antilles</summary>
        AN = 530,

        /// <summary>New Caledonia</summary>
        NC = 540,

        /// <summary>New Zealand</summary>
        NZ = 554,

        /// <summary>Nicaragua</summary>
        NI = 558,

        /// <summary>Niger</summary>
        NE = 562,

        /// <summary>Nigeria</summary>
        NG = 566,

        /// <summary>Niue</summary>
        NU = 570,

        /// <summary>Norfolk Island</summary>
        NF = 574,

        /// <summary>Northern Mariana Islands</summary>
        MP = 580,

        /// <summary>Norway</summary>
        NO = 578,

        /// <summary>Oman</summary>
        OM = 512,

        /// <summary>Pakistan</summary>
        PK = 586,

        /// <summary>Palau</summary>
        PW = 585,

        /// <summary>Palestinian Territory, Occupied</summary>
        PS = 275,

        /// <summary>Panama</summary>
        PA = 591,

        /// <summary>Papua New Guinea</summary>
        PG = 598,

        /// <summary>Paraguay</summary>
        PY = 600,

        /// <summary>Peru</summary>
        PE = 604,

        /// <summary>Philippines</summary>
        PH = 608,

        /// <summary>Pitcairn</summary>
        PN = 612,

        /// <summary>Poland</summary>
        PL = 616,

        /// <summary>Portugal</summary>
        PT = 620,

        /// <summary>Puerto Rico</summary>
        PR = 630,

        /// <summary>Qatar</summary>
        QA = 634,

        /// <summary>Reunion</summary>
        RE = 638,

        /// <summary>Romania</summary>
        RO = 642,

        /// <summary>Russian Federation</summary>
        RU = 643,

        /// <summary>Rwanda</summary>
        RW = 646,

        /// <summary>Saint Helena</summary>
        SH = 654,

        /// <summary>Saint Kitts And Nevis</summary>
        KN = 659,

        /// <summary>Saint Lucia</summary>
        LC = 662,

        /// <summary>Saint Pierre And Miquelon</summary>
        PM = 666,

        /// <summary>Saint Vincent And The Grenadines</summary>
        VC = 670,

        /// <summary>Samoa</summary>
        WS = 882,

        /// <summary>San Marino</summary>
        SM = 674,

        /// <summary>Sao Tome And Principe</summary>
        ST = 678,

        /// <summary>Saudi Arabia</summary>
        SA = 682,

        /// <summary>Senegal</summary>
        SN = 686,

        /// <summary>Serbia And Montenegro</summary>
        CS = 891,

        /// <summary>Seychelles</summary>
        SC = 690,

        /// <summary>Sierra Leone</summary>
        SL = 694,

        /// <summary>Singapore</summary>
        SG = 702,

        /// <summary>Slovakia</summary>
        SK = 703,

        /// <summary>Slovenia</summary>
        SI = 705,

        /// <summary>Solomon Islands</summary>
        SB = 090,

        /// <summary>Somalia</summary>
        SO = 706,

        /// <summary>South Africa</summary>
        ZA = 710,

        /// <summary>South Georgia And The South Sandwich Islands</summary>
        GS = 239,

        /// <summary>Spain</summary>
        ES = 724,

        /// <summary>Sri Lanka</summary>
        LK = 144,

        /// <summary>Sudan</summary>
        SD = 736,

        /// <summary>Suriname</summary>
        SR = 740,

        /// <summary>Svalbard And Jan Mayen</summary>
        SJ = 744,

        /// <summary>Swaziland</summary>
        SZ = 748,

        /// <summary>Sweden</summary>
        SE = 752,

        /// <summary>Switzerland</summary>
        CH = 756,

        /// <summary>Syrian Arab Republic</summary>
        SY = 760,

        /// <summary>Taiwan, Province Of China</summary>
        TW = 158,

        /// <summary>Tajikistan</summary>
        TJ = 762,

        /// <summary>Tanzania, United Republic Of</summary>
        TZ = 834,

        /// <summary>Thailand</summary>
        TH = 764,

        /// <summary>Timor-Leste</summary>
        TL = 626,

        /// <summary>Togo</summary>
        TG = 768,

        /// <summary>Tokelau</summary>
        TK = 772,

        /// <summary>Tonga</summary>
        TO = 776,

        /// <summary>Trinidad And Tobago</summary>
        TT = 780,

        /// <summary>Tunisia</summary>
        TN = 788,

        /// <summary>Turkey</summary>
        TR = 792,

        /// <summary>Turkmenistan</summary>
        TM = 795,

        /// <summary>Turks And Caicos Islands</summary>
        TC = 796,

        /// <summary>Tuvalu</summary>
        TV = 798,

        /// <summary>Uganda</summary>
        UG = 800,

        /// <summary>Ukraine</summary>
        UA = 804,

        /// <summary>United Arab Emirates</summary>
        AE = 784,

        /// <summary>United Kingdom</summary>
        GB = 826,

        /// <summary>United States</summary>
        US = 840,

        /// <summary>United States Minor Outlying Islands</summary>
        UM = 581,

        /// <summary>Uruguay</summary>
        UY = 858,

        /// <summary>Uzbekistan</summary>
        UZ = 860,

        /// <summary>Vanuatu</summary>
        VU = 548,

        /// <summary>Venezuela</summary>
        VE = 862,

        /// <summary>Viet Nam</summary>
        VN = 704,

        /// <summary>Virgin Islands, British</summary>
        VG = 092,

        /// <summary>Virgin Islands, U.S.</summary>
        VI = 850,

        /// <summary>Wallis And Futuna</summary>
        WF = 876,

        /// <summary>Western Sahara</summary>
        EH = 732,

        /// <summary>Yemen</summary>
        YE = 887,

        /// <summary>Zambia</summary>
        ZM = 894,

        /// <summary>Zimbabwe</summary>
        ZW = 716
    }
}