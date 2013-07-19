//------------------------------------------------------------------------------
// <copyright file="CountryCode.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// ISO 3166 country code expressions for international aware item 
    /// validation routines.
    /// </summary>
    [type: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed.")]
    public enum CountryCode : int
    {
        /// <summary>No country specified</summary>
        None = 0,

        /// <summary>Invariant country specified (same as None)</summary>
        Invariant = None,

        /// <summary>Afghanistan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AF = 004,

        /// <summary>Åland Islands</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        AX = 248,

        /// <summary>Albania</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AL = 008,

        /// <summary>Algeria</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        DZ = 012,

        /// <summary>American Samoa</summary>
        AS = 016,

        /// <summary>Andorra</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AD = 020,

        /// <summary>Angola</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AO = 024,

        /// <summary>Anguilla</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AI = 660,

        /// <summary>Antarctica</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AQ = 010,

        /// <summary>Antigua And Barbuda</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AG = 028,

        /// <summary>Argentina</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AR = 032,

        /// <summary>Armenia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AM = 051,

        /// <summary>Aruba</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AW = 533,

        /// <summary>Australia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AU = 036,

        /// <summary>Austria</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AT = 040,

        /// <summary>Azerbaijan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AZ = 031,

        /// <summary>Bahamas</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BS = 044,

        /// <summary>Bahrain</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BH = 048,

        /// <summary>Bangladesh</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BD = 050,

        /// <summary>Barbados</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BB = 052,

        /// <summary>Belarus</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BY = 112,

        /// <summary>Belgium</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BE = 056,

        /// <summary>Belize</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BZ = 084,

        /// <summary>Benin</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BJ = 204,

        /// <summary>Bermuda</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BM = 060,

        /// <summary>Bhutan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BT = 064,

        /// <summary>Bolivia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BO = 068,

        /// <summary>Bosnia And Herzegovina</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BA = 070,

        /// <summary>Botswana</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BW = 072,

        /// <summary>Bouvet Island</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BV = 074,

        /// <summary>Brazil</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BR = 076,

        /// <summary>British Indian Ocean Territory</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IO = 086,

        /// <summary>Brunei Darussalam</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BN = 096,

        /// <summary>Bulgaria</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BG = 100,

        /// <summary>Burkina Faso</summary>
        BF = 854,

        /// <summary>Burundi</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        BI = 108,

        /// <summary>Cambodia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KH = 116,

        /// <summary>Cameroon</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CM = 120,

        /// <summary>Canada</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CA = 124,

        /// <summary>Cape Verde</summary>
        CV = 132,

        /// <summary>Cayman Islands</summary>
        KY = 136,

        /// <summary>Central African Republic</summary>
        CF = 140,

        /// <summary>Chad</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TD = 148,

        /// <summary>Chile</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CL = 152,

        /// <summary>China</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CN = 156,

        /// <summary>Christmas Island</summary>
        CX = 162,

        /// <summary>Cocos (Keeling) Islands</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        CC = 166,

        /// <summary>Colombia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CO = 170,

        /// <summary>Comoros</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KM = 174,

        /// <summary>Congo</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        HR = 191,

        /// <summary>Cuba</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CU = 192,

        /// <summary>Cyprus</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CY = 196,

        /// <summary>Czech Republic</summary>
        CZ = 203,

        /// <summary>Denmark</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        DK = 208,

        /// <summary>Djibouti</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        DJ = 262,

        /// <summary>Dominica</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        DM = 212,

        /// <summary>Dominican Republic</summary>
        DO = 214,

        /// <summary>Ecuador</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        EC = 218,

        /// <summary>Egypt</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        EG = 818,

        /// <summary>El Salvador</summary>
        SV = 222,

        /// <summary>Equatorial Guinea</summary>
        GQ = 226,

        /// <summary>Eritrea</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ER = 232,

        /// <summary>Estonia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        EE = 233,

        /// <summary>Ethiopia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ET = 231,

        /// <summary>Falkland Islands (Malvinas)</summary>
        FK = 238,

        /// <summary>Faroe Islands</summary>
        FO = 234,

        /// <summary>Fiji</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        FJ = 242,

        /// <summary>Finland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        FI = 246,

        /// <summary>France</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        FR = 250,

        /// <summary>French Guiana</summary>
        GF = 254,

        /// <summary>French Polynesia</summary>
        PF = 258,

        /// <summary>French Southern Territories</summary>
        TF = 260,

        /// <summary>Gabon</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GA = 266,

        /// <summary>Gambia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GM = 270,

        /// <summary>Georgia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GE = 268,

        /// <summary>Germany</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        DE = 276,

        /// <summary>Ghana</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GH = 288,

        /// <summary>Gibraltar</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GI = 292,

        /// <summary>Greece</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GR = 300,

        /// <summary>Greenland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GL = 304,

        /// <summary>Grenada</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GD = 308,

        /// <summary>Guadeloupe</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GP = 312,

        /// <summary>Guam</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GU = 316,

        /// <summary>Guatemala</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GT = 320,

        /// <summary>Guinea</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GN = 324,

        /// <summary>Guinea-Bissau</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GW = 624,

        /// <summary>Guyana</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        GY = 328,

        /// <summary>Haiti</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        HT = 332,

        /// <summary>Heard Island And McDonald Islands</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        HM = 334,

        /// <summary>Holy See (Vatican City State)</summary>
        VA = 336,

        /// <summary>Honduras</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        HN = 340,

        /// <summary>Hong Kong</summary>
        HK = 344,

        /// <summary>Hungary</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        HU = 348,

        /// <summary>Iceland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IS = 352,

        /// <summary>India</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IN = 356,

        /// <summary>Indonesia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "This is not an abbreviation but rather the two-letter country code as defined by ISO.")]
        ID = 360,

        /// <summary>Iran, Islamic Republic Of</summary>
        IR = 364,

        /// <summary>Iraq</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IQ = 368,

        /// <summary>Ireland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IE = 372,

        /// <summary>Israel</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IL = 376,

        /// <summary>Italy</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        IT = 380,

        /// <summary>Jamaica</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        JM = 388,

        /// <summary>Japan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        JP = 392,

        /// <summary>Jordan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        JO = 400,

        /// <summary>Kazakhstan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KZ = 398,

        /// <summary>Kenya</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KE = 404,

        /// <summary>Kiribati</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KI = 296,

        /// <summary>Korea, Democratic People'S Republic Of</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KP = 408,

        /// <summary>Korea, Republic Of</summary>
        KR = 410,

        /// <summary>Kuwait</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KW = 414,

        /// <summary>Kyrgyzstan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        KG = 417,

        /// <summary>Lao People'S Democratic Republic</summary>
        LA = 418,

        /// <summary>Latvia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LV = 428,

        /// <summary>Lebanon</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LB = 422,

        /// <summary>Lesotho</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LS = 426,

        /// <summary>Liberia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LR = 430,

        /// <summary>Libyan Arab Jamahiriya</summary>
        LY = 434,

        /// <summary>Liechtenstein</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LI = 438,

        /// <summary>Lithuania</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LT = 440,

        /// <summary>Luxembourg</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        LU = 442,

        /// <summary>Macao</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MO = 446,

        /// <summary>Macedonia, The Former Yugoslav Republic Of</summary>
        MK = 807,

        /// <summary>Madagascar</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MG = 450,

        /// <summary>Malawi</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MW = 454,

        /// <summary>Malaysia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MY = 458,

        /// <summary>Maldives</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MV = 462,

        /// <summary>Mali</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ML = 466,

        /// <summary>Malta</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MT = 470,

        /// <summary>Marshall Islands</summary>
        MH = 584,

        /// <summary>Martinique</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MQ = 474,

        /// <summary>Mauritania</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MR = 478,

        /// <summary>Mauritius</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MU = 480,

        /// <summary>Mayotte</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        YT = 175,

        /// <summary>Mexico</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MX = 484,

        /// <summary>Micronesia, Federated States Of</summary>
        FM = 583,

        /// <summary>Moldova, Republic Of</summary>
        MD = 498,

        /// <summary>Monaco</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MC = 492,

        /// <summary>Mongolia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MN = 496,

        /// <summary>Montserrat</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MS = 500,

        /// <summary>Morocco</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MA = 504,

        /// <summary>Mozambique</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MZ = 508,

        /// <summary>Myanmar</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        MM = 104,

        /// <summary>Namibia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NA = 516,

        /// <summary>Nauru</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NR = 520,

        /// <summary>Nepal</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NP = 524,

        /// <summary>Netherlands</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NL = 528,

        /// <summary>Netherlands Antilles</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        AN = 530,

        /// <summary>New Caledonia</summary>
        NC = 540,

        /// <summary>New Zealand</summary>
        NZ = 554,

        /// <summary>Nicaragua</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NI = 558,

        /// <summary>Niger</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NE = 562,

        /// <summary>Nigeria</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NG = 566,

        /// <summary>Niue</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NU = 570,

        /// <summary>Norfolk Island</summary>
        NF = 574,

        /// <summary>Northern Mariana Islands</summary>
        MP = 580,

        /// <summary>Norway</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        NO = 578,

        /// <summary>Oman</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        OM = 512,

        /// <summary>Pakistan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PK = 586,

        /// <summary>Palau</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PW = 585,

        /// <summary>Palestinian Territory, Occupied</summary>
        PS = 275,

        /// <summary>Panama</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PA = 591,

        /// <summary>Papua New Guinea</summary>
        PG = 598,

        /// <summary>Paraguay</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PY = 600,

        /// <summary>Peru</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PE = 604,

        /// <summary>Philippines</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PH = 608,

        /// <summary>Pitcairn</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PN = 612,

        /// <summary>Poland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PL = 616,

        /// <summary>Portugal</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        PT = 620,

        /// <summary>Puerto Rico</summary>
        PR = 630,

        /// <summary>Qatar</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        QA = 634,

        /// <summary>Reunion</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        RE = 638,

        /// <summary>Romania</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        RO = 642,

        /// <summary>Russian Federation</summary>
        RU = 643,

        /// <summary>Rwanda</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        WS = 882,

        /// <summary>San Marino</summary>
        SM = 674,

        /// <summary>Sao Tome And Principe</summary>
        ST = 678,

        /// <summary>Saudi Arabia</summary>
        SA = 682,

        /// <summary>Senegal</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SN = 686,

        /// <summary>Serbia And Montenegro</summary>
        CS = 891,

        /// <summary>Seychelles</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SC = 690,

        /// <summary>Sierra Leone</summary>
        SL = 694,

        /// <summary>Singapore</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SG = 702,

        /// <summary>Slovakia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SK = 703,

        /// <summary>Slovenia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SI = 705,

        /// <summary>Solomon Islands</summary>
        SB = 090,

        /// <summary>Somalia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SO = 706,

        /// <summary>South Africa</summary>
        ZA = 710,

        /// <summary>South Georgia And The South Sandwich Islands</summary>
        GS = 239,

        /// <summary>Spain</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ES = 724,

        /// <summary>Sri Lanka</summary>
        LK = 144,

        /// <summary>Sudan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SD = 736,

        /// <summary>Suriname</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SR = 740,

        /// <summary>Svalbard And Jan Mayen</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        SJ = 744,

        /// <summary>Swaziland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SZ = 748,

        /// <summary>Sweden</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        SE = 752,

        /// <summary>Switzerland</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        CH = 756,

        /// <summary>Syrian Arab Republic</summary>
        SY = 760,

        /// <summary>Taiwan, Province Of China</summary>
        TW = 158,

        /// <summary>Tajikistan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TJ = 762,

        /// <summary>Tanzania, United Republic Of</summary>
        TZ = 834,

        /// <summary>Thailand</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TH = 764,

        /// <summary>Timor-Leste</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TL = 626,

        /// <summary>Togo</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TG = 768,

        /// <summary>Tokelau</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TK = 772,

        /// <summary>Tonga</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TO = 776,

        /// <summary>Trinidad And Tobago</summary>
        TT = 780,

        /// <summary>Tunisia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TN = 788,

        /// <summary>Turkey</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TR = 792,

        /// <summary>Turkmenistan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TM = 795,

        /// <summary>Turks And Caicos Islands</summary>
        TC = 796,

        /// <summary>Tuvalu</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        TV = 798,

        /// <summary>Uganda</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        UG = 800,

        /// <summary>Ukraine</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        UY = 858,

        /// <summary>Uzbekistan</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        UZ = 860,

        /// <summary>Vanuatu</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        VU = 548,

        /// <summary>Venezuela</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        YE = 887,

        /// <summary>Zambia</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ZM = 894,

        /// <summary>Zimbabwe</summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
        ZW = 716
    }
}