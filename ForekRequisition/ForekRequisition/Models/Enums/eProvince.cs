using System.ComponentModel.DataAnnotations;

namespace ApprenticeApplications.Models.Enums
{
    public enum eProvince
    {
        Mpumalanga,
        Gauteng,
        Limpopo,
        [Display(Name = "Eastern Cape")]
        Eastern_Cape,
        [Display(Name = "Western Cape")]
        Western_Cape,
        [Display(Name = "Northern Cape")]
        Northern_Cape,
        [Display(Name = "North West")]
        North_West,
        [Display(Name = "KwaZulu-Natal")]
        KwaZulu_Natal,
        [Display(Name = "Free State")]
        Free_State
    }
    public enum eDecision
    {
        Yes, No, Unsure
    }
    public enum eProgrammeType
    {
        [Display(Name = "Forek & MEGA Apprentice Opportunity 2022")]
        ForekMegaApprentice,
        [Display(Name = "Forek Artisan Development Opportunity 2022")]
        ForekArtisanDevelopment
    }
    public enum eProgramme
    {
        Electrician,
        [Display(Name = "Electrical Wireman")]
        ElectricalWireman,
        Plumber,
        Welder,
        [Display(Name = "Painter & Decorator")]
        PainterDecorator,
        Bricklayer,
        [Display(Name = "Bricklayer & Plasterer")]
        BricklayerPlasterer,
        Carpenter,
        [Display(Name = "Carpenter & Joiner")]
        CarpenterJoiner,
        Joiner,
        [Display(Name = "Joiner & Woodmachinist")]
        JoinerWoodmachinist,
        Plasterer,
        Tiler

    }
    public enum eTitle
    {
        Mr, Mrs, Miss, Ms, Dr, Jr, Hon, Prof
    }
    public enum eStatus
    {
        Submitted,
        Approved,
        Rejected
    }
    public enum eRole
    {
        [Display(Name = "System Administrator")]
        SystemAdministrator,
        Applicant
    }
    public enum eRace
    {
        African,
        Colored,
        White,
        Indian,
        Asian
    }
    public enum eGender
    {
        Male, Female
    }
    public enum ePassedSubj
    {
        Yes,
        No,
        Pending
    }
    public enum eMunicipality
    {
        [Display(Name = "Mbombela Municipality")]
        Mbombela_Municipality,
        [Display(Name = "Sol Plaatjie")]
        Sol_Plaatjie_Municipality,
        [Display(Name = "Bushbuckidge Local Municipality")]
        Bushbuckidge,
        [Display(Name = "Nkomazi Local Municipality")]
        Nkomazi,
        [Display(Name = "Thaba Chweu Local Municipality")]
        ThabaChweu,
        [Display(Name = "Chief Albert Luthuli Local Municipality")]
        ChiefAlbert,
        [Display(Name = "Dipaleseng Local Municipality")]
        Dipaleseng,
        [Display(Name = "Dr Pixley Ka Isaka Seme Local Municipality")]
        DrPixley,
        [Display(Name = "Govan Mbeki Local Municipality")]
        GovanMbeki,
        [Display(Name = "Lekwa Local Municipality")]
        Lekwa,
        [Display(Name = "Mkhondo Local Municipality")]
        Mkhondo,
        [Display(Name = "Msukaligwa Local Municipality")]
        Msukaligwa,
        [Display(Name = "Dr JS Moroka Local Municipality")]
        DrJSMoroka,
        [Display(Name = "Emakhazeni Local Municipality")]
        Emakahazeni,
        [Display(Name = "Steve Tshwete Local Municipality")]
        SteveTshwete,
        [Display(Name = "Emalahleni Local Municipality")]
        Emalahleni,
        [Display(Name = "Thembisile Hani Local Municipality")]
        ThembisileHani,
        [Display(Name = "Victor Khanye Local Municipality")]
        VictorKhanye,


        [Display(Name = "Matatiele Local Municipality")]
        Matatiele,
        [Display(Name = "Ntabankulu Local Municipality")]
        Ntabankulu,
        [Display(Name = "Umzimvubu Local Municipality")]
        Umzimvubu,
        [Display(Name = "Winnie Madikizela-Mandela Local Municipality")]
        WinnieMM,
        [Display(Name = "Amahlathi Local Municipality")]
        Amahlathi,
        [Display(Name = "Great Kei Local Municipality")]
        GreatKei,
        [Display(Name = "Mbhashe Local Municipality")]
        Mbhashe,
        [Display(Name = "Mnquma Local Municipality")]
        Mnquma,
        [Display(Name = "Ngqushwa Local Municipality")]
        Ngqushwa,
        [Display(Name = "Raymond Mhlaba Local Municipality")]
        RaymondMhlaba,
        [Display(Name = "Emalahleni Local Municipality")]
        Emalahlani,
        [Display(Name = "Engcobo Local Municipality")]
        Engcobo,
        [Display(Name = "Enoch Mgijima Local Municipality")]
        EnochMgijima,
        [Display(Name = "Intsika Yethu Local Municipality")]
        IntsikaYethu,
        [Display(Name = "Inxuba Yethemba Local Municipality")]
        InxubaYethemba,
        [Display(Name = "Sakhisizwe Local Municipality")]
        Sakhisizwe,
        [Display(Name = "Elundi Local Municipality")]
        Elundi,
        [Display(Name = "Senqu Local Municipality")]
        Senqu,
        [Display(Name = "Walter Sisulu Local Municipality")]
        WalterSisulu,
        [Display(Name = "Ingquza Hill Local Municipality")]
        IngquzaHill,
        [Display(Name = "King Sabata Dalindyebo Local Municipality")]
        KingSabataD,
        [Display(Name = "Mhlontlo Local Municipality")]
        Mhlontlo,
        [Display(Name = "Nyandeni Local Municipality")]
        Nyadeni,
        [Display(Name = "Port St Johns Local Municipality")]
        PortStJohns,
        [Display(Name = "Blue Crane Route Local Municipality")]
        BlueCraneRoute,
        [Display(Name = "Dr Beyers Naude Local Municipality")]
        DrbeyersNaude,
        [Display(Name = "Kouga Local Municipality")]
        Kouga,
        [Display(Name = "Koukamma Local Municipality")]
        Koukamma,
        [Display(Name = "Makana Local Municipality")]
        Makana,
        [Display(Name = "Ndlambe Local Municipality")]
        Ndlambe,
        [Display(Name = "Sundays RiverValley Local Municipality")]
        SundaysRiverValley,

        [Display(Name = "Emfuleni Local Municipality")]
        Emfuleni,
        [Display(Name = "Lesedi Local Municipality")]
        Lesedi,
        [Display(Name = "Midvaal Local Municipality")]
        Midvaal,
        [Display(Name = "Merafong City Local Municipality")]
        MerafongCity,
        [Display(Name = "Mogale City Local Municipality")]
        Mogalecity,
        [Display(Name = "Rand West City Local Municipality")]
        RandwestCity




    }

    public enum eMathSubj
    {
        Mathematics,
        [Display(Name = "Mathematics Literacy")]
        MathematicsLiteracy,
        [Display(Name = "Technical Mathematics")]
        TechnicalMaths
    }
    public enum eGradePassed
    {
        [Display(Name = "Grade 9")]
        Grade9,
        [Display(Name = "Grade 10")]
        Grade10,
        [Display(Name = "Grade 11")]
        Grade11,
        [Display(Name = "Grade 12")]
        Grade12
    }
    public enum eQualificationType
    {
        [Display(Name = "Nated Studies")]
        Nated_Qualification,
        [Display(Name = "NCV Qualification")]
        NCV_Qualification,
        Other

    }
    public enum eQualifLevel
    {
        [Display(Name = "NCV 2")]
        Level2,
        [Display(Name = "NCV 3")]
        Level3,
        [Display(Name = "NCV 4")]
        Level4,
        N1, N2, N3, N4, N5, N6,
        [Display(Name = "Advanced Diploma")]
        AdvancedDip,
        [Display(Name = "Bachelors Degree")]
        Degree,
        [Display(Name = "Honors Degree")]
        Hons,
        [Display(Name = "Masters Degree")]
        Masters,
        PhD,Other,None
    }
    public enum eMunicipality2
    {
        AlbertLuthuli,
        Bushbuckridge,
        Dipaleseng,
        Ehlanzeni,
        Emakhazeni,
        Emalahleni,
        [Display(Name = "Gert Sibande")]
        GertSibande,
        [Display(Name = "Govan Mbeki")]
        GovanMbeki,
        [Display(Name = "JS Moroka")]
        JSMoroka,
        [Display(Name = "Lekwa")]
        LekwaMunicipality,
        Mbombela,
        Mkhondo,
        Msukaligwa,
        Nkangala,
        Nkomazi,
        [Display(Name = "Pixley KaSeme")]
        PixleyKaSeme,
        [Display(Name = "Steve Tshwete")]
        SteveTshwete,
        [Display(Name = "Thaba Tshweu")]
        ThabaTshweu,
        [Display(Name = "Thembisile Hani")]
        ThembisileHani,
        [Display(Name = "Victor Khanye")]
        VictorKhanye




    }
    public enum eDistrict
    {
        [Display(Name = "Ehlanzeni District")]
        Ehlanzeni,
        [Display(Name = "Gert Sibande District")]
        Gert,
        [Display(Name = "Nkangala District")]
        Nkangala
    }
    public enum eApproval
    {
        [Display(Name = "Not Approved")]
        NotApproved,
        Approved
    }
    public enum eAddressType
    {
        [Display(Name = "Postal Address")]
        Postal,
        [Display(Name = "Residential Address")]
        Residential
    }
    public enum eRelationship
    {
        Mother,Father,Aunt,Uncle,
        Caretaker,Brother,Sister,Cousin,
        Grandmother,Grandfather,Husband, Wife,
        [Display(Name = "Brother In-Law" )]
        BrotherInLaw,
        [Display(Name = "Sister In-Law")]
        SisterInLaw,
        [Display(Name = "Mother In-Law")]
        MotherInLaw,
        [Display(Name = "Father In-Law")]
        FatherInLaw,
        Pastor,Mentor, Other
    }
    public enum eCourse
    {
        [Display(Name = "ARPL & Trade Test (Plumber)")]
        ARPLTTPlumber,
        [Display(Name = "ARPL & Trade Test (Welder)")]
        ARPLTTWelder,
        [Display(Name = "ARPL & Trade Test (Electrician)")]
        ARPLTTElectr,
        [Display(Name = "ARPL & Trade Test (Painter)")]
        ARPLTTPainter,
        [Display(Name = "ARPL & Trade Test (Plasterer & Tiler)")]
        ARPLTTPlasterer,
        [Display(Name = "ARPL & Trade Test (Bricklayer)")]
        ARPLTTBricklayer,
        [Display(Name = "Occupational Trade: (Plumber)")]
        OccupationalPlumber,
        [Display(Name = "Welder")]
        OccupationalWelder,
        [Display(Name = "Electrician")]
        OccupationalElectrician,
        [Display(Name = "Bricklayer")]
        OccupationalBricklayer,
        [Display(Name = "Painter")]
        OccupationalPainter,
        [Display(Name = "Plastere & Tiler")]
        OccupationalPlastereTiler,
        [Display(Name = "Office Administrator")]
        OfficeAdmin,
        Bookkeeper,
        [Display(Name = "Occupational Health & Safety Practitioner [OHS]")]
        OHS,
        [Display(Name = "Pest Management Officer")]
        Pest,
        [Display(Name = "Supply Chain Practitioner")]
        SupplyChain,
        [Display(Name = "Project Manager")]
        ProjectManagement,
        [Display(Name = "Engineering (N1)")]
        N1Engineering,
        [Display(Name = "Engineering (N2)")]
        N2Engineering,
        [Display(Name = "Engineering (N3)")]
        N3Engineering,
        [Display(Name = "Engineering (N4)")]
        N4Engineering,
        [Display(Name = "Engineering (N5)")]
        N5Engineering,
        [Display(Name = "Engineering (N6)")]
        N6Engineering,
        [Display(Name = "Business Studies (N4)")]
        N4Business,
        [Display(Name = "Business Studies (N5)")]
        N5Business,
        [Display(Name = "Business Studies (N6)")]
        N6Business,
        Abattoir,
        [Display(Name = "Animal Production")]
        AnimalProduction,
        [Display(Name = "Pant Production")]
        PlantProduction,
        [Display(Name = "Poultry Production")]
        PoultryProduction,
        Mechanisation,
        [Display(Name = "Meat Examination")]
        MeatExamination,
        [Display(Name = "Pest Control")]
        PestControl





    }
    public enum eFunding
    {
        CETA,
        [Display(Name = "Chairman's Bursary")]
        Chairman,
        [Display(Name = "National Skills Fund")]
        NSF,
        [Display(Name = "Private - (Self-Paying)")]
        Private,
        Other
    }
    public enum eStudyMode
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name = "Part Time")]
        PartTime,
        Blended
    }

    public enum eNationality
    {
        [Display(Name = "South Africa")]
        SouthAfrica,
        Other
    }


}
