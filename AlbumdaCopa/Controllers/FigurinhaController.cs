using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using AlbumdaCopa.Models;

namespace AlbumdaCopa.Controllers
{
    public class PlayerEntry
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }

    public class FigurinhaController
    {
        private readonly SQLiteConnection _database;
        private static readonly Random _random = new Random();

        // todos os 757 jogadores com nomes e imagens
        public static readonly PlayerEntry[] PoolJogadores = new PlayerEntry[]
        {
            new PlayerEntry { Name = "AARON_WAN_BISSAKA", Path = "aaron_wan_bissaka.jpg" },
            new PlayerEntry { Name = "ABBOSBEK_FAVZULLAEV", Path = "abbosbek_favzullaev.jpg" },
            new PlayerEntry { Name = "ABDE_EZZALZOULI", Path = "abde_ezzalzouli.jpg" },
            new PlayerEntry { Name = "ABDOULAYE_SECK", Path = "abdoulaye_seck.jpg" },
            new PlayerEntry { Name = "ABDULAZIZ_RATEM", Path = "abdulaziz_ratem.jpg" },
            new PlayerEntry { Name = "ABDULKERIM_BARDAKCI", Path = "abdulkerim_bardakci.jpg" },
            new PlayerEntry { Name = "ABDULLAH_ALKHAIBARI", Path = "abdullah_alkhaibari.jpg" },
            new PlayerEntry { Name = "ABDULRAHMAN_ALOBUD", Path = "abdulrahman_alobud.jpg" },
            new PlayerEntry { Name = "ABDULRAHMAN_ALSANBI", Path = "abdulrahman_alsanbi.jpg" },
            new PlayerEntry { Name = "ABDUL_ISSAHAKU_FATAWU", Path = "abdul_issahaku_fatawu.jpg" },
            new PlayerEntry { Name = "ADALBERTO_CARRASOUILLA", Path = "adalberto_carrasouilla.jpg" },
            new PlayerEntry { Name = "ADAM_HLOIEK", Path = "adam_hloiek.jpg" },
            new PlayerEntry { Name = "ADAM_MASINA", Path = "adam_masina.jpg" },
            new PlayerEntry { Name = "ADRIEN_RABIOT", Path = "adrien_rabiot.jpg" },
            new PlayerEntry { Name = "AHMED_AL_GANEHI", Path = "ahmed_al_ganehi.jpg" },
            new PlayerEntry { Name = "AHMED_FATOUH", Path = "ahmed_fatouh.jpg" },
            new PlayerEntry { Name = "AIDEN_ONEILL", Path = "aiden_oneill.jpg" },
            new PlayerEntry { Name = "AIMAR_SHER", Path = "aimar_sher.jpg" },
            new PlayerEntry { Name = "AIMOTHV_CASTAGNE", Path = "aimothv_castagne.jpg" },
            new PlayerEntry { Name = "AISSA_MANDI", Path = "aissa_mandi.jpg" },
            new PlayerEntry { Name = "AKAM_HASHEM", Path = "akam_hashem.jpg" },
            new PlayerEntry { Name = "ALAN_FRANCO", Path = "alan_franco.jpg" },
            new PlayerEntry { Name = "ALAN_MINDA", Path = "alan_minda.jpg" },
            new PlayerEntry { Name = "ALBERTO_QUINTERO", Path = "alberto_quintero.jpg" },
            new PlayerEntry { Name = "ALESSANDRO_CIRCATI", Path = "alessandro_circati.jpg" },
            new PlayerEntry { Name = "ALEXANDER_DJIKU", Path = "alexander_djiku.jpg" },
            new PlayerEntry { Name = "ALEXANDER_PRASS", Path = "alexander_prass.jpg" },
            new PlayerEntry { Name = "ALEXANDER_SAK", Path = "alexander_sak.jpg" },
            new PlayerEntry { Name = "ALEXANDER_SCHLAGER", Path = "alexander_schlager.jpg" },
            new PlayerEntry { Name = "ALEXIS_GUENDOUZ", Path = "alexis_guendouz.jpg" },
            new PlayerEntry { Name = "ALEXIS_MAC_ALLISTER", Path = "alexis_mac_allister.jpg" },
            new PlayerEntry { Name = "ALEXIS_SAELEMAEKERS", Path = "alexis_saelemaekers.jpg" },
            new PlayerEntry { Name = "ALEXIS_VEGA", Path = "alexis_vega.jpg" },
            new PlayerEntry { Name = "ALEX_FREEMAN", Path = "alex_freeman.jpg" },
            new PlayerEntry { Name = "ALEX_PAULSEN", Path = "alex_paulsen.jpg" },
            new PlayerEntry { Name = "ALIOU_SEIDU", Path = "aliou_seidu.jpg" },
            new PlayerEntry { Name = "ALIREZA_JAHANBAKHSH", Path = "alireza_jahanbakhsh.jpg" },
            new PlayerEntry { Name = "ALISSON_BECKER", Path = "alisson_becker.jpg" },
            new PlayerEntry { Name = "ALI_ABDI", Path = "ali_abdi.jpg" },
            new PlayerEntry { Name = "ALI_AL_HAMADI", Path = "ali_al_hamadi.jpg" },
            new PlayerEntry { Name = "ALI_JASIM", Path = "ali_jasim.jpg" },
            new PlayerEntry { Name = "ALI_OLWAN", Path = "ali_olwan.jpg" },
            new PlayerEntry { Name = "ALMOEZ_ALI", Path = "almoez_ali.jpg" },
            new PlayerEntry { Name = "ALPHONSO_DAVIES", Path = "alphonso_davies.jpg" },
            new PlayerEntry { Name = "ALSSA_LAIDOUNI", Path = "alssa_laidouni.jpg" },
            new PlayerEntry { Name = "ALVARO_MORATA", Path = "alvaro_morata.jpg" },
            new PlayerEntry { Name = "AMADOU_ONANA", Path = "amadou_onana.jpg" },
            new PlayerEntry { Name = "AMAD_DIALLO", Path = "amad_diallo.jpg" },
            new PlayerEntry { Name = "AMAR_MEMIC", Path = "amar_memic.jpg" },
            new PlayerEntry { Name = "AMER_JAMOUS", Path = "amer_jamous.jpg" },
            new PlayerEntry { Name = "AMINE_GOUIRI", Path = "amine_gouiri.jpg" },
            new PlayerEntry { Name = "AMIR_HADZIAHMETOVIC", Path = "amir_hadziahmetovic.jpg" },
            new PlayerEntry { Name = "ANDREAS_SCHJELDERUP", Path = "andreas_schjelderup.jpg" },
            new PlayerEntry { Name = "ANDREO_KRAMARIC", Path = "andreo_kramaric.jpg" },
            new PlayerEntry { Name = "ANDRES_CUBAS", Path = "andres_cubas.jpg" },
            new PlayerEntry { Name = "ANGELO_PRECIADO", Path = "angelo_preciado.jpg" },
            new PlayerEntry { Name = "ANGUS_GUNN", Path = "angus_gunn.jpg" },
            new PlayerEntry { Name = "ANIBAL_GODOV", Path = "anibal_godov.jpg" },
            new PlayerEntry { Name = "ANIS_HADJ_MOUSSA", Path = "anis_hadj_moussa.jpg" },
            new PlayerEntry { Name = "ANTE_BUDIMIR", Path = "ante_budimir.jpg" },
            new PlayerEntry { Name = "ANTHONV_ELANGA", Path = "anthonv_elanga.jpg" },
            new PlayerEntry { Name = "ANTHONY_GORDON", Path = "anthony_gordon.jpg" },
            new PlayerEntry { Name = "ANTHONY_RALSTON", Path = "anthony_ralston.jpg" },
            new PlayerEntry { Name = "ANTOINE_SEMENVO", Path = "antoine_semenvo.jpg" },
            new PlayerEntry { Name = "ANTONEE_ROBINSON", Path = "antonee_robinson.jpg" },
            new PlayerEntry { Name = "ANTONIO_NUSA", Path = "antonio_nusa.jpg" },
            new PlayerEntry { Name = "ANTONIO_RODIGER", Path = "antonio_rodiger.jpg" },
            new PlayerEntry { Name = "AO_TANAKA", Path = "ao_tanaka.jpg" },
            new PlayerEntry { Name = "ARDA_GULER", Path = "arda_guler.jpg" },
            new PlayerEntry { Name = "ARDON_JASHARI", Path = "ardon_jashari.jpg" },
            new PlayerEntry { Name = "ARMANDO_OBISPO", Path = "armando_obispo.jpg" },
            new PlayerEntry { Name = "ARON_DANNUM", Path = "aron_dannum.jpg" },
            new PlayerEntry { Name = "ARTHUR_MASUAKU", Path = "arthur_masuaku.jpg" },
            new PlayerEntry { Name = "ARTHUR_THEATE", Path = "arthur_theate.jpg" },
            new PlayerEntry { Name = "ASSIM_MADIBO", Path = "assim_madibo.jpg" },
            new PlayerEntry { Name = "ASTON_MAVELE", Path = "aston_mavele.jpg" },
            new PlayerEntry { Name = "AURELE_AMENDA", Path = "aurele_amenda.jpg" },
            new PlayerEntry { Name = "AURELIEN_TCHOUAMENI", Path = "aurelien_tchouameni.jpg" },
            new PlayerEntry { Name = "AUREZA_BEIRANVAND", Path = "aureza_beiranvand.jpg" },
            new PlayerEntry { Name = "AVASE_UEDA", Path = "avase_ueda.jpg" },
            new PlayerEntry { Name = "AVMEN_DAHMEN", Path = "avmen_dahmen.jpg" },
            new PlayerEntry { Name = "AXEL_TUANZEBE", Path = "axel_tuanzebe.jpg" },
            new PlayerEntry { Name = "AYMERIC_LAPORTE", Path = "aymeric_laporte.jpg" },
            new PlayerEntry { Name = "AYOUB_EL_KAABI", Path = "ayoub_el_kaabi.jpg" },
            new PlayerEntry { Name = "AZIZBEK_TURGUNBOEV", Path = "azizbek_turgunboev.jpg" },
            new PlayerEntry { Name = "AZIZ_BEHICH", Path = "aziz_behich.jpg" },
            new PlayerEntry { Name = "BAGHDAD_BOUNEDJAH", Path = "baghdad_bounedjah.jpg" },
            new PlayerEntry { Name = "BARI_ALPER_VILMAZ", Path = "bari_alper_vilmaz.jpg" },
            new PlayerEntry { Name = "BART_VERBRUGGEN", Path = "bart_verbruggen.jpg" },
            new PlayerEntry { Name = "BELGICA", Path = "belgica.jpg" },
            new PlayerEntry { Name = "BENJAMIN TAHIROVIC", Path = "benjamin_tahirovic.jpg" },
            new PlayerEntry { Name = "BENSEBAINI", Path = "bensebaini.jpg" },
            new PlayerEntry { Name = "BENTO", Path = "bento.jpg" },
            new PlayerEntry { Name = "BERNARDO SILVA", Path = "bernardo_silva.jpg" },
            new PlayerEntry { Name = "BILAL EL KHANNOUSS", Path = "bilal_el_khannouss.jpg" },
            new PlayerEntry { Name = "BOUALEM KHOUKHI", Path = "boualem_khoukhi.jpg" },
            new PlayerEntry { Name = "BOULAVE DIA", Path = "boulave_dia.jpg" },
            new PlayerEntry { Name = "BRADLEV BARCOLA", Path = "bradlev_barcola.jpg" },
            new PlayerEntry { Name = "BRAHIM DIAZ", Path = "brahim_diaz.jpg" },
            new PlayerEntry { Name = "BRANDON MECHELE", Path = "brandon_mechele.jpg" },
            new PlayerEntry { Name = "BRENDEN AARONSON", Path = "brenden_aaronson.jpg" },
            new PlayerEntry { Name = "BRIAN CIPENGA", Path = "brian_cipenga.jpg" },
            new PlayerEntry { Name = "BRUNO FERNANDES", Path = "bruno_fernandes.jpg" },
            new PlayerEntry { Name = "BRUNO GUIMARAES", Path = "bruno_guimaraes.jpg" },
            new PlayerEntry { Name = "BUKAVO SAKA", Path = "bukavo_saka.jpg" },
            new PlayerEntry { Name = "CAGLAR SOVONCO", Path = "caglar_sovonco.jpg" },
            new PlayerEntry { Name = "CALEB VIRENKVI", Path = "caleb_virenkvi.jpg" },
            new PlayerEntry { Name = "CALLUM MCCOWATT", Path = "callum_mccowatt.jpg" },
            new PlayerEntry { Name = "CAMERON BURGESS", Path = "cameron_burgess.jpg" },
            new PlayerEntry { Name = "CAMILO VARGAS", Path = "camilo_vargas.jpg" },
            new PlayerEntry { Name = "CAN UZUN", Path = "can_uzun.jpg" },
            new PlayerEntry { Name = "CARLENS ARCUS", Path = "carlens_arcus.jpg" },
            new PlayerEntry { Name = "CARLOS HARVEV", Path = "carlos_harvev.jpg" },
            new PlayerEntry { Name = "CARLOS RODRIGUEZ", Path = "carlos_rodriguez.jpg" },
            new PlayerEntry { Name = "CASEMIRO", Path = "casemiro.jpg" },
            new PlayerEntry { Name = "CEDRLC BAKAMBU", Path = "cedrlc_bakambu.jpg" },
            new PlayerEntry { Name = "CESAR BLACKMAN", Path = "cesar_blackman.jpg" },
            new PlayerEntry { Name = "CESAR HUERTA", Path = "cesar_huerta.jpg" },
            new PlayerEntry { Name = "CESAR MONTES", Path = "cesar_montes.jpg" },
            new PlayerEntry { Name = "CHANCEL MBEMBA", Path = "chancel_mbemba.jpg" },
            new PlayerEntry { Name = "CHARLES ACKEL", Path = "charles_ackel.jpg" },
            new PlayerEntry { Name = "CHRIS RICHARDS", Path = "chris_richards.jpg" },
            new PlayerEntry { Name = "CHRIS WOOD", Path = "chris_wood.jpg" },
            new PlayerEntry { Name = "CHRISTIAN PULISIC", Path = "christian_pulisic.jpg" },
            new PlayerEntry { Name = "CHRISTOPH BAUMGARTNER", Path = "christoph_baumgartner.jpg" },
            new PlayerEntry { Name = "CHRISTOPHER ATTVS", Path = "christopher_attvs.jpg" },
            new PlayerEntry { Name = "CODV GAKPO", Path = "codv_gakpo.jpg" },
            new PlayerEntry { Name = "COLE PALMER", Path = "cole_palmer.jpg" },
            new PlayerEntry { Name = "CRAIG GOODWIN", Path = "craig_goodwin.jpg" },
            new PlayerEntry { Name = "CRISTIAN MARTINEZ", Path = "cristian_martinez.jpg" },
            new PlayerEntry { Name = "CRISTIAN ROLDAN", Path = "cristian_roldan.jpg" },
            new PlayerEntry { Name = "CRISTIAN ROMERO", Path = "cristian_romero.jpg" },
            new PlayerEntry { Name = "CRISTIANO RONALDO", Path = "cristiano_ronaldo.jpg" },
            new PlayerEntry { Name = "CRISTOPH BAUMGARTNER", Path = "cristoph_baumgartner.jpg" },
            new PlayerEntry { Name = "CURACAO", Path = "curacao.jpg" },
            new PlayerEntry { Name = "CYLE LARIN", Path = "cyle_larin.jpg" },
            new PlayerEntry { Name = "DAICHI KAMADA", Path = "daichi_kamada.jpg" },
            new PlayerEntry { Name = "DAILON LIVRAMENTO", Path = "dailon_livramento.jpg" },
            new PlayerEntry { Name = "DAILON LIVRAMENTO_2", Path = "dailon_livramento_2.jpg" },
            new PlayerEntry { Name = "DAN BURN", Path = "dan_burn.jpg" },
            new PlayerEntry { Name = "DAN NDOVE", Path = "dan_ndove.jpg" },
            new PlayerEntry { Name = "DANI CARVAJAL", Path = "dani_carvajal.jpg" },
            new PlayerEntry { Name = "DANI OLMO", Path = "dani_olmo.jpg" },
            new PlayerEntry { Name = "DANIEL MUNOZ", Path = "daniel_munoz.jpg" },
            new PlayerEntry { Name = "DANIEL SVENSSON", Path = "daniel_svensson.jpg" },
            new PlayerEntry { Name = "DANILO", Path = "danilo.jpg" },
            new PlayerEntry { Name = "DANLEV JEAN JACQUES", Path = "danlev_jean_jacques.jpg" },
            new PlayerEntry { Name = "DARWIN NUNEZ", Path = "darwin_nunez.jpg" },
            new PlayerEntry { Name = "DAVID ALABA", Path = "david_alaba.jpg" },
            new PlayerEntry { Name = "DAVID MOLLER WOLFE", Path = "david_moller_wolfe.jpg" },
            new PlayerEntry { Name = "DAVID OSPINA", Path = "david_ospina.jpg" },
            new PlayerEntry { Name = "DAVID ZIMA", Path = "david_zima.jpg" },
            new PlayerEntry { Name = "DAVINSON SANCHEZ", Path = "davinson_sanchez.jpg" },
            new PlayerEntry { Name = "DAVNE ST CLAIR", Path = "davne_st_clair.jpg" },
            new PlayerEntry { Name = "DAVOT UPAMECANO", Path = "davot_upamecano.jpg" },
            new PlayerEntry { Name = "DEAN HUIJSEN", Path = "dean_huijsen.jpg" },
            new PlayerEntry { Name = "DECLAN RICE", Path = "declan_rice.jpg" },
            new PlayerEntry { Name = "DENIS ZAKARIA", Path = "denis_zakaria.jpg" },
            new PlayerEntry { Name = "DENZEL DUMFRIES", Path = "denzel_dumfries.jpg" },
            new PlayerEntry { Name = "DEREK CORNELIUS", Path = "derek_cornelius.jpg" },
            new PlayerEntry { Name = "DEROV DUARTE", Path = "derov_duarte.jpg" },
            new PlayerEntry { Name = "DERRICK ETIENNE JR", Path = "derrick_etienne_jr.jpg" },
            new PlayerEntry { Name = "DESIRE DOUE", Path = "desire_doue.jpg" },
            new PlayerEntry { Name = "DIEGO GOMEZ", Path = "diego_gomez.jpg" },
            new PlayerEntry { Name = "DIEGO LAINEZ", Path = "diego_lainez.jpg" },
            new PlayerEntry { Name = "DIEGO LUNA", Path = "diego_luna.jpg" },
            new PlayerEntry { Name = "DINEV", Path = "dinev.jpg" },
            new PlayerEntry { Name = "DIOGO COSTA", Path = "diogo_costa.jpg" },
            new PlayerEntry { Name = "DIOGO DALOT", Path = "diogo_dalot.jpg" },
            new PlayerEntry { Name = "DONVELL MALEN", Path = "donvell_malen.jpg" },
            new PlayerEntry { Name = "DUCKENS NAZON", Path = "duckens_nazon.jpg" },
            new PlayerEntry { Name = "DUJE CALETA-CAR", Path = "duje_caleta_car.jpg" },
            new PlayerEntry { Name = "DUKE LACROIX", Path = "duke_lacroix.jpg" },
            new PlayerEntry { Name = "EDER MILITAO", Path = "eder_militao.jpg" },
            new PlayerEntry { Name = "EDGAR BARCENAS", Path = "edgar_barcenas.jpg" },
            new PlayerEntry { Name = "EDIN DIEKO", Path = "edin_dieko.jpg" },
            new PlayerEntry { Name = "EDO KAVEMBE", Path = "edo_kavembe.jpg" },
            new PlayerEntry { Name = "EDOUARD MENDV", Path = "edouard_mendv.jpg" },
            new PlayerEntry { Name = "EDSON ALVAREZ", Path = "edson_alvarez.jpg" },
            new PlayerEntry { Name = "EDUARDO CAMAVINGA", Path = "eduardo_camavinga.jpg" },
            new PlayerEntry { Name = "ELDOR SHOMURODOV", Path = "eldor_shomurodov.jpg" },
            new PlayerEntry { Name = "ELIAS ACHOURI", Path = "elias_achouri.jpg" },
            new PlayerEntry { Name = "ELIAS SAAD", Path = "elias_saad.jpg" },
            new PlayerEntry { Name = "ELIESSE BEN SEGHIR", Path = "eliesse_ben_seghir.jpg" },
            new PlayerEntry { Name = "ELLVES SKHIRI", Path = "ellves_skhiri.jpg" },
            new PlayerEntry { Name = "ELOV ROOM", Path = "elov_room.jpg" },
            new PlayerEntry { Name = "EMILIANO MARTINEZ", Path = "emiliano_martinez.jpg" },
            new PlayerEntry { Name = "EMMANUEL AGBADOU", Path = "emmanuel_agbadou.jpg" },
            new PlayerEntry { Name = "ENNER VALENCIA", Path = "enner_valencia.jpg" },
            new PlayerEntry { Name = "ENZO FERNANDEZ", Path = "enzo_fernandez.jpg" },
            new PlayerEntry { Name = "ERIC DAVIS", Path = "eric_davis.jpg" },
            new PlayerEntry { Name = "ERICK SANCHEZ", Path = "erick_sanchez.jpg" },
            new PlayerEntry { Name = "ERLING HAALAND", Path = "erling_haaland.jpg" },
            new PlayerEntry { Name = "ESCOCIA", Path = "escocia.jpg" },
            new PlayerEntry { Name = "ESPANHA", Path = "espanha.jpg" },
            new PlayerEntry { Name = "ESTEVAO", Path = "estevao.jpg" },
            new PlayerEntry { Name = "EVAN NDICKA", Path = "evan_ndicka.jpg" },
            new PlayerEntry { Name = "EVAN NDICKA_2", Path = "evan_ndicka_2.jpg" },
            new PlayerEntry { Name = "EVANN GUESSAND", Path = "evann_guessand.jpg" },
            new PlayerEntry { Name = "EXEOUIEL PALACIOS", Path = "exeouiel_palacios.jpg" },
            new PlayerEntry { Name = "EZRI KONSA", Path = "ezri_konsa.jpg" },
            new PlayerEntry { Name = "FABIAN BALBUENA", Path = "fabian_balbuena.jpg" },
            new PlayerEntry { Name = "FABIAN RIEDER", Path = "fabian_rieder.jpg" },
            new PlayerEntry { Name = "FABIAN RUIZ", Path = "fabian_ruiz.jpg" },
            new PlayerEntry { Name = "FACUNDO PELLISTRI", Path = "facundo_pellistri.jpg" },
            new PlayerEntry { Name = "FARES CHAIN", Path = "fares_chain.jpg" },
            new PlayerEntry { Name = "FARRUKH SAVFIEV", Path = "farrukh_savfiev.jpg" },
            new PlayerEntry { Name = "FEDERICO VALVERDE", Path = "federico_valverde.jpg" },
            new PlayerEntry { Name = "FEDERICO VINAS", Path = "federico_vinas.jpg" },
            new PlayerEntry { Name = "FELIX NMECHA", Path = "felix_nmecha.jpg" },
            new PlayerEntry { Name = "FERDI KADIOCLU", Path = "ferdi_kadioclu.jpg" },
            new PlayerEntry { Name = "FERJANI SASSI", Path = "ferjani_sassi.jpg" },
            new PlayerEntry { Name = "FERRAN TORRES", Path = "ferran_torres.jpg" },
            new PlayerEntry { Name = "FIDEL ESCOBAR", Path = "fidel_escobar.jpg" },
            new PlayerEntry { Name = "FIFA WORL", Path = "fifa_worl.jpg" },
            new PlayerEntry { Name = "FINN SURMAN", Path = "finn_surman.jpg" },
            new PlayerEntry { Name = "FLORIAN WIRTZ", Path = "florian_wirtz.jpg" },
            new PlayerEntry { Name = "FOLARIN BALOGUN", Path = "folarin_balogun.jpg" },
            new PlayerEntry { Name = "FRANCIS DE VRIES", Path = "francis_de_vries.jpg" },
            new PlayerEntry { Name = "FRANCISCO TRINCAO", Path = "francisco_trincao.jpg" },
            new PlayerEntry { Name = "FRANCO MASTANTUONO", Path = "franco_mastantuono.jpg" },
            new PlayerEntry { Name = "FRANJO IVANOVIC", Path = "franjo_ivanovic.jpg" },
            new PlayerEntry { Name = "FRANTZDY PIERROT", Path = "frantzdy_pierrot.jpg" },
            new PlayerEntry { Name = "FRENKIE DE JONG", Path = "frenkie_de_jong.jpg" },
            new PlayerEntry { Name = "GABRIEL GUDMUNDSSON", Path = "gabriel_gudmundsson.jpg" },
            new PlayerEntry { Name = "GABRIEL MAGALHAES", Path = "gabriel_magalhaes.jpg" },
            new PlayerEntry { Name = "GABRIEL MARTINELLI", Path = "gabriel_martinelli.jpg" },
            new PlayerEntry { Name = "GARRV RODRIGUES", Path = "garrv_rodrigues.jpg" },
            new PlayerEntry { Name = "GERVANE KASTANEER", Path = "gervane_kastaneer.jpg" },
            new PlayerEntry { Name = "GHISLAIN KONAN", Path = "ghislain_konan.jpg" },
            new PlayerEntry { Name = "GIULIANO SIMEONE", Path = "giuliano_simeone.jpg" },
            new PlayerEntry { Name = "GODFRIED ROEMERATOE", Path = "godfried_roemeratoe.jpg" },
            new PlayerEntry { Name = "GONCALO INACIO", Path = "goncalo_inacio.jpg" },
            new PlayerEntry { Name = "GONCALO RAMOS", Path = "goncalo_ramos.jpg" },
            new PlayerEntry { Name = "GONZALO PLATA", Path = "gonzalo_plata.jpg" },
            new PlayerEntry { Name = "GONZALO VALLE", Path = "gonzalo_valle.jpg" },
            new PlayerEntry { Name = "GRANIT XHAKA", Path = "granit_xhaka.jpg" },
            new PlayerEntry { Name = "GRANT HANLEV", Path = "grant_hanlev.jpg" },
            new PlayerEntry { Name = "GREGOR KOBEL", Path = "gregor_kobel.jpg" },
            new PlayerEntry { Name = "GUEVE", Path = "gueve.jpg" },
            new PlayerEntry { Name = "GUILLERMO VARELA", Path = "guillermo_varela.jpg" },
            new PlayerEntry { Name = "GUSTAVO GOMEZ", Path = "gustavo_gomez.jpg" },
            new PlayerEntry { Name = "HABIB DIARRA", Path = "habib_diarra.jpg" },
            new PlayerEntry { Name = "HAJI WRIGHT", Path = "haji_wright.jpg" },
            new PlayerEntry { Name = "HAJI WRIGHT_2", Path = "haji_wright_2.jpg" },
            new PlayerEntry { Name = "HAKAN CALHANOCW", Path = "hakan_calhanocw.jpg" },
            new PlayerEntry { Name = "HANBEOM LEE", Path = "hanbeom_lee.jpg" },
            new PlayerEntry { Name = "HANNES DELCROIX", Path = "hannes_delcroix.jpg" },
            new PlayerEntry { Name = "HANNIBAL MEJBRI", Path = "hannibal_mejbri.jpg" },
            new PlayerEntry { Name = "HANS VANAKEN", Path = "hans_vanaken.jpg" },
            new PlayerEntry { Name = "HARRY KANE", Path = "harry_kane.jpg" },
            new PlayerEntry { Name = "HARRY SOUTTAR", Path = "harry_souttar.jpg" },
            new PlayerEntry { Name = "HASSAN AL-HAVDOS", Path = "hassan_al_havdos.jpg" },
            new PlayerEntry { Name = "HASSAN ALTAMBAKTI", Path = "hassan_altambakti.jpg" },
            new PlayerEntry { Name = "HAZEM MASTOURI", Path = "hazem_mastouri.jpg" },
            new PlayerEntry { Name = "HEECHAN HWANG", Path = "heechan_hwang.jpg" },
            new PlayerEntry { Name = "HERNAN GALINDEZ", Path = "hernan_galindez.jpg" },
            new PlayerEntry { Name = "HEUNGMIN SON", Path = "heungmin_son.jpg" },
            new PlayerEntry { Name = "HICHAM BOUDAOUI", Path = "hicham_boudaoui.jpg" },
            new PlayerEntry { Name = "HIRVING LOZANO", Path = "hirving_lozano.jpg" },
            new PlayerEntry { Name = "HOMAM AHMED", Path = "homam_ahmed.jpg" },
            new PlayerEntry { Name = "HOSSEIN KANAANI", Path = "hossein_kanaani.jpg" },
            new PlayerEntry { Name = "HOUSSEM AOUAR", Path = "houssem_aouar.jpg" },
            new PlayerEntry { Name = "HUGO EKITIKE", Path = "hugo_ekitike.jpg" },
            new PlayerEntry { Name = "HUGO LARSSON", Path = "hugo_larsson.jpg" },
            new PlayerEntry { Name = "HUSNIDDIN ALIOULOV", Path = "husniddin_alioulov.jpg" },
            new PlayerEntry { Name = "HUSSEIN ALI", Path = "hussein_ali.jpg" },
            new PlayerEntry { Name = "HVEONGVU OH", Path = "hveongvu_oh.jpg" },
            new PlayerEntry { Name = "HYEONWOO JO", Path = "hyeonwoo_jo.jpg" },
            new PlayerEntry { Name = "IBRAHIM BAVESH", Path = "ibrahim_bavesh.jpg" },
            new PlayerEntry { Name = "IBRAHIM SAADEH", Path = "ibrahim_saadeh.jpg" },
            new PlayerEntry { Name = "IBRAHIM SABRA", Path = "ibrahim_sabra.jpg" },
            new PlayerEntry { Name = "IBRAHIM SANGARE", Path = "ibrahim_sangare.jpg" },
            new PlayerEntry { Name = "IBRAHIMA KONATE", Path = "ibrahima_konate.jpg" },
            new PlayerEntry { Name = "IDRISSA GANA GUEVE", Path = "idrissa_gana_gueve.jpg" },
            new PlayerEntry { Name = "IFAWORLDC P", Path = "ifaworldc_p.jpg" },
            new PlayerEntry { Name = "IGOR SERGEEV", Path = "igor_sergeev.jpg" },
            new PlayerEntry { Name = "IHSAN HADDAD", Path = "ihsan_haddad.jpg" },
            new PlayerEntry { Name = "INAKI WILLIAMS", Path = "inaki_williams.jpg" },
            new PlayerEntry { Name = "INAKI WILLIAMS_2", Path = "inaki_williams_2.jpg" },
            new PlayerEntry { Name = "IORAAM RAVNERS", Path = "ioraam_ravners.jpg" },
            new PlayerEntry { Name = "IRFAN CAN KAHVECI", Path = "irfan_can_kahveci.jpg" },
            new PlayerEntry { Name = "ISAK HIEN", Path = "isak_hien.jpg" },
            new PlayerEntry { Name = "ISMAEL BENNACER", Path = "ismael_bennacer.jpg" },
            new PlayerEntry { Name = "ISMAEL GHARBI", Path = "ismael_gharbi.jpg" },
            new PlayerEntry { Name = "ISMAEL KONE", Path = "ismael_kone.jpg" },
            new PlayerEntry { Name = "ISMAEL SAIBARI", Path = "ismael_saibari.jpg" },
            new PlayerEntry { Name = "ISMAIL JAKOBS", Path = "ismail_jakobs.jpg" },
            new PlayerEntry { Name = "ISMAIL VUKSEK", Path = "ismail_vuksek.jpg" },
            new PlayerEntry { Name = "ISMAILA SARR", Path = "ismaila_sarr.jpg" },
            new PlayerEntry { Name = "ISRAEL REVES", Path = "israel_reves.jpg" },
            new PlayerEntry { Name = "IVAN BASIC", Path = "ivan_basic.jpg" },
            new PlayerEntry { Name = "IVAN PERISLE", Path = "ivan_perisle.jpg" },
            new PlayerEntry { Name = "IVAN SUNJIC", Path = "ivan_sunjic.jpg" },
            new PlayerEntry { Name = "JACK HENDRV", Path = "jack_hendrv.jpg" },
            new PlayerEntry { Name = "JACKSON IRVINE", Path = "jackson_irvine.jpg" },
            new PlayerEntry { Name = "JAESUNG LEE", Path = "jaesung_lee.jpg" },
            new PlayerEntry { Name = "JALAL HASSAN", Path = "jalal_hassan.jpg" },
            new PlayerEntry { Name = "JALOLIDDIN MASHARIPOV", Path = "jaloliddin_masharipov.jpg" },
            new PlayerEntry { Name = "JAMAL MUSIALA", Path = "jamal_musiala.jpg" },
            new PlayerEntry { Name = "JAMES RODRIGUEZ", Path = "james_rodriguez.jpg" },
            new PlayerEntry { Name = "JAMSHID ISKANDEROV", Path = "jamshid_iskanderov.jpg" },
            new PlayerEntry { Name = "JAN PAUL VAN HECKE", Path = "jan_paul_van_hecke.jpg" },
            new PlayerEntry { Name = "JAPAO", Path = "japao.jpg" },
            new PlayerEntry { Name = "JAROSLAV ZELENV", Path = "jaroslav_zelenv.jpg" },
            new PlayerEntry { Name = "JAWAD EL VAMIO", Path = "jawad_el_vamio.jpg" },
            new PlayerEntry { Name = "JEAN-KEVIN DUVERNE", Path = "jean_kevin_duverne.jpg" },
            new PlayerEntry { Name = "JEAN-PHILIPPE GBAMIN", Path = "jean_philippe_gbamin.jpg" },
            new PlayerEntry { Name = "JEAN-RICNER BELLEGARDE", Path = "jean_ricner_bellegarde.jpg" },
            new PlayerEntry { Name = "JEARL MARGARITHA", Path = "jearl_margaritha.jpg" },
            new PlayerEntry { Name = "JEFFERSON LERMA", Path = "jefferson_lerma.jpg" },
            new PlayerEntry { Name = "JEHAD THIKRI", Path = "jehad_thikri.jpg" },
            new PlayerEntry { Name = "JENS CASTROP", Path = "jens_castrop.jpg" },
            new PlayerEntry { Name = "JEREMIE FRIMPONG", Path = "jeremie_frimpong.jpg" },
            new PlayerEntry { Name = "JEREMV ANTONISSE", Path = "jeremv_antonisse.jpg" },
            new PlayerEntry { Name = "JEREMV DOW", Path = "jeremv_dow.jpg" },
            new PlayerEntry { Name = "JESPER KARLSTROM", Path = "jesper_karlstrom.jpg" },
            new PlayerEntry { Name = "JESUS GALLARDO", Path = "jesus_gallardo.jpg" },
            new PlayerEntry { Name = "JHON ARIAS", Path = "jhon_arias.jpg" },
            new PlayerEntry { Name = "JHON CORDOBA", Path = "jhon_cordoba.jpg" },
            new PlayerEntry { Name = "JHON LUCUMI", Path = "jhon_lucumi.jpg" },
            new PlayerEntry { Name = "JIJNYA ITO", Path = "jijnya_ito.jpg" },
            new PlayerEntry { Name = "JINDAICH STANEK", Path = "jindaich_stanek.jpg" },
            new PlayerEntry { Name = "JOAO CANCELO", Path = "joao_cancelo.jpg" },
            new PlayerEntry { Name = "JOAO FELIX", Path = "joao_felix.jpg" },
            new PlayerEntry { Name = "JOAO NEVES", Path = "joao_neves.jpg" },
            new PlayerEntry { Name = "JOAO PAULO", Path = "joao_paulo.jpg" },
            new PlayerEntry { Name = "JOAO PEDRO", Path = "joao_pedro.jpg" },
            new PlayerEntry { Name = "JOE BELL", Path = "joe_bell.jpg" },
            new PlayerEntry { Name = "JOE BELL_2", Path = "joe_bell_2.jpg" },
            new PlayerEntry { Name = "JOEL ORDONEZ", Path = "joel_ordonez.jpg" },
            new PlayerEntry { Name = "JOHAN MANZAMBI", Path = "johan_manzambi.jpg" },
            new PlayerEntry { Name = "JOHAN MOJICA", Path = "johan_mojica.jpg" },
            new PlayerEntry { Name = "JOHAN VASQUEZ", Path = "johan_vasquez.jpg" },
            new PlayerEntry { Name = "JOHN MCGLNN", Path = "john_mcglnn.jpg" },
            new PlayerEntry { Name = "JOHN SOUTTAR", Path = "john_souttar.jpg" },
            new PlayerEntry { Name = "JOHN STONES", Path = "john_stones.jpg" },
            new PlayerEntry { Name = "JOHN YEBOAH", Path = "john_yeboah.jpg" },
            new PlayerEntry { Name = "JOHNV PLACIDE", Path = "johnv_placide.jpg" },
            new PlayerEntry { Name = "JONATHAN DAVID", Path = "jonathan_david.jpg" },
            new PlayerEntry { Name = "JONATHAN OSORIO", Path = "jonathan_osorio.jpg" },
            new PlayerEntry { Name = "JONATHAN TAH", Path = "jonathan_tah.jpg" },
            new PlayerEntry { Name = "JORDAN AVEW", Path = "jordan_avew.jpg" },
            new PlayerEntry { Name = "JORDAN BOS", Path = "jordan_bos.jpg" },
            new PlayerEntry { Name = "JORDAN HENDERSON", Path = "jordan_henderson.jpg" },
            new PlayerEntry { Name = "JORDAN PICKFORD", Path = "jordan_pickford.jpg" },
            new PlayerEntry { Name = "JORGE CARRASCAL", Path = "jorge_carrascal.jpg" },
            new PlayerEntry { Name = "JORGE SANCHEZ", Path = "jorge_sanchez.jpg" },
            new PlayerEntry { Name = "JORGEN STRAND LARSEN", Path = "jorgen_strand_larsen.jpg" },
            new PlayerEntry { Name = "JORIS KAVEMBE", Path = "joris_kavembe.jpg" },
            new PlayerEntry { Name = "JOSE CORDOBA", Path = "jose_cordoba.jpg" },
            new PlayerEntry { Name = "JOSE LUIS RODRIGUEZ", Path = "jose_luis_rodriguez.jpg" },
            new PlayerEntry { Name = "JOSE MARIA GIMENEZ", Path = "jose_maria_gimenez.jpg" },
            new PlayerEntry { Name = "JOSE SA", Path = "jose_sa.jpg" },
            new PlayerEntry { Name = "JOSEPH PAINTSIL", Path = "joseph_paintsil.jpg" },
            new PlayerEntry { Name = "JOSHUA BRENET", Path = "joshua_brenet.jpg" },
            new PlayerEntry { Name = "JOSKO GVARDIOL", Path = "josko_gvardiol.jpg" },
            new PlayerEntry { Name = "JOSUE CASIMIR", Path = "josue_casimir.jpg" },
            new PlayerEntry { Name = "JOVANE CABRAL", Path = "jovane_cabral.jpg" },
            new PlayerEntry { Name = "JUAN FERNANDO OUINTERO", Path = "juan_fernando_ouintero.jpg" },
            new PlayerEntry { Name = "JUAN JOSE CACERES", Path = "juan_jose_caceres.jpg" },
            new PlayerEntry { Name = "JUDE BELLINGHAM", Path = "jude_bellingham.jpg" },
            new PlayerEntry { Name = "JUDE BELLINGHAM_2", Path = "jude_bellingham_2.jpg" },
            new PlayerEntry { Name = "JULES KOUNDE", Path = "jules_kounde.jpg" },
            new PlayerEntry { Name = "JULIAN ALVAREZ", Path = "julian_alvarez.jpg" },
            new PlayerEntry { Name = "JULIO ENCISO", Path = "julio_enciso.jpg" },
            new PlayerEntry { Name = "JUNINHO BACUNA", Path = "juninho_bacuna.jpg" },
            new PlayerEntry { Name = "JUNIOR ALONSO", Path = "junior_alonso.jpg" },
            new PlayerEntry { Name = "JUNNOSUKE SUZUKI", Path = "junnosuke_suzuki.jpg" },
            new PlayerEntry { Name = "JURGEN LOCADIA", Path = "jurgen_locadia.jpg" },
            new PlayerEntry { Name = "JURIEN GAARI", Path = "jurien_gaari.jpg" },
            new PlayerEntry { Name = "JURRIEN TIMBER", Path = "jurrien_timber.jpg" },
            new PlayerEntry { Name = "JUSTIN KLUIVERT", Path = "justin_kluivert.jpg" },
            new PlayerEntry { Name = "KAAN AVHAN", Path = "kaan_avhan.jpg" },
            new PlayerEntry { Name = "KAI HAVERTZ", Path = "kai_havertz.jpg" },
            new PlayerEntry { Name = "KAISHU SANO", Path = "kaishu_sano.jpg" },
            new PlayerEntry { Name = "KALIDOU KOULIBALV", Path = "kalidou_koulibalv.jpg" },
            new PlayerEntry { Name = "KAMAL MILLER", Path = "kamal_miller.jpg" },
            new PlayerEntry { Name = "KAMALDEEN SULEMANA", Path = "kamaldeen_sulemana.jpg" },
            new PlayerEntry { Name = "KANGIN LEE", Path = "kangin_lee.jpg" },
            new PlayerEntry { Name = "KARIM BOUDIAF", Path = "karim_boudiaf.jpg" },
            new PlayerEntry { Name = "KEITO NAKAMURA", Path = "keito_nakamura.jpg" },
            new PlayerEntry { Name = "KEN SEMA", Path = "ken_sema.jpg" },
            new PlayerEntry { Name = "KENAN VILDIZ", Path = "kenan_vildiz.jpg" },
            new PlayerEntry { Name = "KENDRV PAEZ", Path = "kendrv_paez.jpg" },
            new PlayerEntry { Name = "KENJI CORRE", Path = "kenji_corre.jpg" },
            new PlayerEntry { Name = "KENW MCLEAN", Path = "kenw_mclean.jpg" },
            new PlayerEntry { Name = "KEREM AKTURKOCLU", Path = "kerem_akturkoclu.jpg" },
            new PlayerEntry { Name = "KEVIN CASTANO", Path = "kevin_castano.jpg" },
            new PlayerEntry { Name = "KEVIN DANSO", Path = "kevin_danso.jpg" },
            new PlayerEntry { Name = "KEVIN DE BRUYNE", Path = "kevin_de_bruyne.jpg" },
            new PlayerEntry { Name = "KEVIN PINA", Path = "kevin_pina.jpg" },
            new PlayerEntry { Name = "KEVIN RODRIGUEZ", Path = "kevin_rodriguez.jpg" },
            new PlayerEntry { Name = "KHALED SOBHI", Path = "khaled_sobhi.jpg" },
            new PlayerEntry { Name = "KHALED SOBHI_2", Path = "khaled_sobhi_2.jpg" },
            new PlayerEntry { Name = "KHOJIAKBAR ALIJONOV", Path = "khojiakbar_alijonov.jpg" },
            new PlayerEntry { Name = "KHOJIMAT ERKINOV", Path = "khojimat_erkinov.jpg" },
            new PlayerEntry { Name = "KHULISO MUDAU", Path = "khuliso_mudau.jpg" },
            new PlayerEntry { Name = "KHULISO MUDAU_2", Path = "khuliso_mudau_2.jpg" },
            new PlayerEntry { Name = "KHULUMANI NDAMANE", Path = "khulumani_ndamane.jpg" },
            new PlayerEntry { Name = "KINGSLEV COMAN", Path = "kingslev_coman.jpg" },
            new PlayerEntry { Name = "KONRAD LAIMER", Path = "konrad_laimer.jpg" },
            new PlayerEntry { Name = "KOSTA BARBAROUSES", Path = "kosta_barbarouses.jpg" },
            new PlayerEntry { Name = "KREPIN DIATTA", Path = "krepin_diatta.jpg" },
            new PlayerEntry { Name = "KRISTIJAN JAKIC", Path = "kristijan_jakic.jpg" },
            new PlayerEntry { Name = "KRISTOFFER VASSBAKK AJER", Path = "kristoffer_vassbakk_ajer.jpg" },
            new PlayerEntry { Name = "KRISTOFFER VASSBAKK AJER_2", Path = "kristoffer_vassbakk_ajer_2.jpg" },
            new PlayerEntry { Name = "KUSINI VENGI", Path = "kusini_vengi.jpg" },
            new PlayerEntry { Name = "KYLIAN MBAPPE", Path = "kylian_mbappe.jpg" },
            new PlayerEntry { Name = "LADISLAV KREJCI", Path = "ladislav_krejci.jpg" },
            new PlayerEntry { Name = "LAMINE CAMARA", Path = "lamine_camara.jpg" },
            new PlayerEntry { Name = "LAMINE VAMAL", Path = "lamine_vamal.jpg" },
            new PlayerEntry { Name = "LAUTARO MARTINEZ", Path = "lautaro_martinez.jpg" },
            new PlayerEntry { Name = "LEANDRO PAREDES", Path = "leandro_paredes.jpg" },
            new PlayerEntry { Name = "LEO BSTIGARD", Path = "leo_bstigard.jpg" },
            new PlayerEntry { Name = "LEON GORETZKA", Path = "leon_goretzka.jpg" },
            new PlayerEntry { Name = "LEONARDO BALERDI", Path = "leonardo_balerdi.jpg" },
            new PlayerEntry { Name = "LEONARDO CAMPANA", Path = "leonardo_campana.jpg" },
            new PlayerEntry { Name = "LEVERTON PIERRE", Path = "leverton_pierre.jpg" },
            new PlayerEntry { Name = "LEWIS FERGUSON", Path = "lewis_ferguson.jpg" },
            new PlayerEntry { Name = "LEWIS MILLER", Path = "lewis_miller.jpg" },
            new PlayerEntry { Name = "LIAM MILLAR", Path = "liam_millar.jpg" },
            new PlayerEntry { Name = "LIBERATO CACACE", Path = "liberato_cacace.jpg" },
            new PlayerEntry { Name = "LIMAN NDIAVE", Path = "liman_ndiave.jpg" },
            new PlayerEntry { Name = "LIONEL MESSI", Path = "lionel_messi.jpg" },
            new PlayerEntry { Name = "LIONEL MESSI_2", Path = "lionel_messi_2.jpg" },
            new PlayerEntry { Name = "LIONEL MPASI", Path = "lionel_mpasi.jpg" },
            new PlayerEntry { Name = "LOGAN COSTA", Path = "logan_costa.jpg" },
            new PlayerEntry { Name = "LOIS OPENDA", Path = "lois_openda.jpg" },
            new PlayerEntry { Name = "LOUICIUS DEEDSON", Path = "louicius_deedson.jpg" },
            new PlayerEntry { Name = "LOVRO MAJER", Path = "lovro_majer.jpg" },
            new PlayerEntry { Name = "LUCAS BERGVALL", Path = "lucas_bergvall.jpg" },
            new PlayerEntry { Name = "LUCAS DIGNE", Path = "lucas_digne.jpg" },
            new PlayerEntry { Name = "LUCAS MENDES", Path = "lucas_mendes.jpg" },
            new PlayerEntry { Name = "LUCAS PAQUETA", Path = "lucas_paqueta.jpg" },
            new PlayerEntry { Name = "LUIS DIAZ", Path = "luis_diaz.jpg" },
            new PlayerEntry { Name = "LUIS MALAGON", Path = "luis_malagon.jpg" },
            new PlayerEntry { Name = "LUIS MEJIA", Path = "luis_mejia.jpg" },
            new PlayerEntry { Name = "LUIS SUAREZ", Path = "luis_suarez.jpg" },
            new PlayerEntry { Name = "LUIZ HENRIQUE", Path = "luiz_henrique.jpg" },
            new PlayerEntry { Name = "LUKA MODRIC", Path = "luka_modric.jpg" },
            new PlayerEntry { Name = "LUKA MODRIC_2", Path = "luka_modric_2.jpg" },
            new PlayerEntry { Name = "LUKA VUSKOVIC", Path = "luka_vuskovic.jpg" },
            new PlayerEntry { Name = "LUKAS CERV", Path = "lukas_cerv.jpg" },
            new PlayerEntry { Name = "LVNDON DVKES", Path = "lvndon_dvkes.jpg" },
            new PlayerEntry { Name = "MAHMOUD AL-MARDI", Path = "mahmoud_al_mardi.jpg" },
            new PlayerEntry { Name = "MALIK TILLMAN", Path = "malik_tillman.jpg" },
            new PlayerEntry { Name = "MALIK TILLMAN_2", Path = "malik_tillman_2.jpg" },
            new PlayerEntry { Name = "MANAF VOUNIS", Path = "manaf_vounis.jpg" },
            new PlayerEntry { Name = "MANCHESTER FCENGI", Path = "manchester_fcengi.jpg" },
            new PlayerEntry { Name = "MANU KONE", Path = "manu_kone.jpg" },
            new PlayerEntry { Name = "MANUEL AKANJI", Path = "manuel_akanji.jpg" },
            new PlayerEntry { Name = "MANUEL UGARTE", Path = "manuel_ugarte.jpg" },
            new PlayerEntry { Name = "MARC CUCURELLA", Path = "marc_cucurella.jpg" },
            new PlayerEntry { Name = "MARC GUEHI", Path = "marc_guehi.jpg" },
            new PlayerEntry { Name = "MARC-ANDRE TER STEGEN", Path = "marc_andre_ter_stegen.jpg" },
            new PlayerEntry { Name = "MARCEL RUIZ", Path = "marcel_ruiz.jpg" },
            new PlayerEntry { Name = "MARCEL SABITZER", Path = "marcel_sabitzer.jpg" },
            new PlayerEntry { Name = "MARCUS RASHFORD", Path = "marcus_rashford.jpg" },
            new PlayerEntry { Name = "MARIIN ODEGAARD", Path = "mariin_odegaard.jpg" },
            new PlayerEntry { Name = "MARIO PASALIC", Path = "mario_pasalic.jpg" },
            new PlayerEntry { Name = "MARK MCKENZIE", Path = "mark_mckenzie.jpg" },
            new PlayerEntry { Name = "MARKO FARJI", Path = "marko_farji.jpg" },
            new PlayerEntry { Name = "MARKO STAMENIC", Path = "marko_stamenic.jpg" },
            new PlayerEntry { Name = "MARQUINHOS", Path = "marquinhos.jpg" },
            new PlayerEntry { Name = "MARTIN BATURINA", Path = "martin_baturina.jpg" },
            new PlayerEntry { Name = "MARTIN EXPERIENCE", Path = "martin_experience.jpg" },
            new PlayerEntry { Name = "MARTIN ZUBIMENDI", Path = "martin_zubimendi.jpg" },
            new PlayerEntry { Name = "MARWAN ALSAHAFI", Path = "marwan_alsahafi.jpg" },
            new PlayerEntry { Name = "MATEJ KOVAK", Path = "matej_kovak.jpg" },
            new PlayerEntry { Name = "MATEJ VVDRA", Path = "matej_vvdra.jpg" },
            new PlayerEntry { Name = "MATHEUS CUNHA", Path = "matheus_cunha.jpg" },
            new PlayerEntry { Name = "MATHEW RVAN", Path = "mathew_rvan.jpg" },
            new PlayerEntry { Name = "MATHIAS OLIVERA", Path = "mathias_olivera.jpg" },
            new PlayerEntry { Name = "MATHIAS VILLASANTI", Path = "mathias_villasanti.jpg" },
            new PlayerEntry { Name = "MATHIEU CHOINIERE", Path = "mathieu_choiniere.jpg" },
            new PlayerEntry { Name = "MATT FREESE", Path = "matt_freese.jpg" },
            new PlayerEntry { Name = "MATTHEW GARBETT", Path = "matthew_garbett.jpg" },
            new PlayerEntry { Name = "MAX CROCOMBE", Path = "max_crocombe.jpg" },
            new PlayerEntry { Name = "MAXI ARAUJO", Path = "maxi_araujo.jpg" },
            new PlayerEntry { Name = "MAXIM DE CUVPER", Path = "maxim_de_cuvper.jpg" },
            new PlayerEntry { Name = "MAXIMILIAN MITTELSTADT", Path = "maximilian_mittelstadt.jpg" },
            new PlayerEntry { Name = "MEHDI TAREMI", Path = "mehdi_taremi.jpg" },
            new PlayerEntry { Name = "MEMPHIS DEPAV", Path = "memphis_depav.jpg" },
            new PlayerEntry { Name = "MERCHAS DOSKI", Path = "merchas_doski.jpg" },
            new PlayerEntry { Name = "MERIH DEMIRAL", Path = "merih_demiral.jpg" },
            new PlayerEntry { Name = "MERT MOLDUR", Path = "mert_moldur.jpg" },
            new PlayerEntry { Name = "MESCHACK ELIA", Path = "meschack_elia.jpg" },
            new PlayerEntry { Name = "MEXICO", Path = "mexico.jpg" },
            new PlayerEntry { Name = "MICHAEL AMIR MURILLO", Path = "michael_amir_murillo.jpg" },
            new PlayerEntry { Name = "MICHAEL BOXALL", Path = "michael_boxall.jpg" },
            new PlayerEntry { Name = "MICHAEL GREGORITSCH", Path = "michael_gregoritsch.jpg" },
            new PlayerEntry { Name = "MICHAEL OLISE", Path = "michael_olise.jpg" },
            new PlayerEntry { Name = "MICHAL SADILEK", Path = "michal_sadilek.jpg" },
            new PlayerEntry { Name = "MICHEL AEBISCHER", Path = "michel_aebischer.jpg" },
            new PlayerEntry { Name = "MICKV VAN DE VEN", Path = "mickv_van_de_ven.jpg" },
            new PlayerEntry { Name = "MIGUEL ALMIRON", Path = "miguel_almiron.jpg" },
            new PlayerEntry { Name = "MIKE MAGNAN", Path = "mike_magnan.jpg" },
            new PlayerEntry { Name = "MIKEL MERINO", Path = "mikel_merino.jpg" },
            new PlayerEntry { Name = "MIKEL OYARZABAL", Path = "mikel_oyarzabal.jpg" },
            new PlayerEntry { Name = "MILAD MOHAMMADI", Path = "milad_mohammadi.jpg" },
            new PlayerEntry { Name = "MILOS DEGENEK", Path = "milos_degenek.jpg" },
            new PlayerEntry { Name = "MOHAMED ALI BEN ROMDHANE", Path = "mohamed_ali_ben_romdhane.jpg" },
            new PlayerEntry { Name = "MOHAMED AMINE TOUCAI", Path = "mohamed_amine_toucai.jpg" },
            new PlayerEntry { Name = "MOHAMED ELSHENAWV", Path = "mohamed_elshenawv.jpg" },
            new PlayerEntry { Name = "MOHAMED HAMDV", Path = "mohamed_hamdv.jpg" },
            new PlayerEntry { Name = "MOHAMED SALAH", Path = "mohamed_salah.jpg" },
            new PlayerEntry { Name = "MOHAMMAD ABU HASHISH", Path = "mohammad_abu_hashish.jpg" },
            new PlayerEntry { Name = "MOHAMMAD ABU ZRAVO", Path = "mohammad_abu_zravo.jpg" },
            new PlayerEntry { Name = "MOHAMMAD ABUALNADI", Path = "mohammad_abualnadi.jpg" },
            new PlayerEntry { Name = "MOHAMMAD MOHEBI", Path = "mohammad_mohebi.jpg" },
            new PlayerEntry { Name = "MOHAMMED AMOURA", Path = "mohammed_amoura.jpg" },
            new PlayerEntry { Name = "MOHAMMED SALISU", Path = "mohammed_salisu.jpg" },
            new PlayerEntry { Name = "MOHAMMED WAAD", Path = "mohammed_waad.jpg" },
            new PlayerEntry { Name = "MOHANAD ALI", Path = "mohanad_ali.jpg" },
            new PlayerEntry { Name = "MOHANAD LASHEEN", Path = "mohanad_lasheen.jpg" },
            new PlayerEntry { Name = "MOHANNAD ABU TAHA", Path = "mohannad_abu_taha.jpg" },
            new PlayerEntry { Name = "MOHAU NKOTA", Path = "mohau_nkota.jpg" },
            new PlayerEntry { Name = "MOISE BOMBITO", Path = "moise_bombito.jpg" },
            new PlayerEntry { Name = "MOISES CAICEDO", Path = "moises_caicedo.jpg" },
            new PlayerEntry { Name = "MONTASSAR TALBI", Path = "montassar_talbi.jpg" },
            new PlayerEntry { Name = "MORGAN ROGERS", Path = "morgan_rogers.jpg" },
            new PlayerEntry { Name = "MORTEN THORSBV", Path = "morten_thorsbv.jpg" },
            new PlayerEntry { Name = "MORTEZA POURALIGANJI", Path = "morteza_pouraliganji.jpg" },
            new PlayerEntry { Name = "MOTEB ALHARBI", Path = "moteb_alharbi.jpg" },
            new PlayerEntry { Name = "MOUSA AL-TAAMARI", Path = "mousa_al_taamari.jpg" },
            new PlayerEntry { Name = "MOUSSA NIAKHATE", Path = "moussa_niakhate.jpg" },
            new PlayerEntry { Name = "MUSAB ALJUWAVR", Path = "musab_aljuwavr.jpg" },
            new PlayerEntry { Name = "MVUNGJAE LEE", Path = "mvungjae_lee.jpg" },
            new PlayerEntry { Name = "NAHITAN NANDEZ", Path = "nahitan_nandez.jpg" },
            new PlayerEntry { Name = "NAHUEL MOLINA", Path = "nahuel_molina.jpg" },
            new PlayerEntry { Name = "NAIM SLITI", Path = "naim_sliti.jpg" },
            new PlayerEntry { Name = "NASSER ALDAWSARI", Path = "nasser_aldawsari.jpg" },
            new PlayerEntry { Name = "NATHAN AKE", Path = "nathan_ake.jpg" },
            new PlayerEntry { Name = "NATHANAEL MBUKU", Path = "nathanael_mbuku.jpg" },
            new PlayerEntry { Name = "NAVEF AGUERD", Path = "navef_aguerd.jpg" },
            new PlayerEntry { Name = "NESTORV IRANKUNDA", Path = "nestorv_irankunda.jpg" },
            new PlayerEntry { Name = "NGALAVEL MUKAU", Path = "ngalavel_mukau.jpg" },
            new PlayerEntry { Name = "NICK WOLTEMADE", Path = "nick_woltemade.jpg" },
            new PlayerEntry { Name = "NICO ELVEDI", Path = "nico_elvedi.jpg" },
            new PlayerEntry { Name = "NICO GONZALEZ", Path = "nico_gonzalez.jpg" },
            new PlayerEntry { Name = "NICO PAZ", Path = "nico_paz.jpg" },
            new PlayerEntry { Name = "NICO SCHLOTTERBECK", Path = "nico_schlotterbeck.jpg" },
            new PlayerEntry { Name = "NICO WILLIAMS", Path = "nico_williams.jpg" },
            new PlayerEntry { Name = "NICOLAS JACKSON", Path = "nicolas_jackson.jpg" },
            new PlayerEntry { Name = "NICOLAS OTAMENDI", Path = "nicolas_otamendi.jpg" },
            new PlayerEntry { Name = "NICOLAS RASKIN", Path = "nicolas_raskin.jpg" },
            new PlayerEntry { Name = "NICOLAS SEIWALD", Path = "nicolas_seiwald.jpg" },
            new PlayerEntry { Name = "NICOLAS TAGLIAFICO", Path = "nicolas_tagliafico.jpg" },
            new PlayerEntry { Name = "NIHAD MUJAKIC", Path = "nihad_mujakic.jpg" },
            new PlayerEntry { Name = "NIKO SIGUR", Path = "niko_sigur.jpg" },
            new PlayerEntry { Name = "NIKOLA VASILJ", Path = "nikola_vasilj.jpg" },
            new PlayerEntry { Name = "NILSON ANGULO", Path = "nilson_angulo.jpg" },
            new PlayerEntry { Name = "NIZAR AL-RASHDAN", Path = "nizar_al_rashdan.jpg" },
            new PlayerEntry { Name = "NME U A", Path = "nme_u_a.jpg" },
            new PlayerEntry { Name = "NOOR AL-RAWABDEH", Path = "noor_al_rawabdeh.jpg" },
            new PlayerEntry { Name = "NUNO MENDES", Path = "nuno_mendes.jpg" },
            new PlayerEntry { Name = "ODILON KOSSOUNOU", Path = "odilon_kossounou.jpg" },
            new PlayerEntry { Name = "OLLIE WATKINS", Path = "ollie_watkins.jpg" },
            new PlayerEntry { Name = "OMAR ALDERETE", Path = "omar_alderete.jpg" },
            new PlayerEntry { Name = "OMAR MARMOUSH", Path = "omar_marmoush.jpg" },
            new PlayerEntry { Name = "OMID NOORAFKAN", Path = "omid_noorafkan.jpg" },
            new PlayerEntry { Name = "ORBELFN PINEDA", Path = "orbelfn_pineda.jpg" },
            new PlayerEntry { Name = "ORLANDO GILL", Path = "orlando_gill.jpg" },
            new PlayerEntry { Name = "OSAMA RASHID", Path = "osama_rashid.jpg" },
            new PlayerEntry { Name = "OSCAR BOBB", Path = "oscar_bobb.jpg" },
            new PlayerEntry { Name = "OSMAN BUKARI", Path = "osman_bukari.jpg" },
            new PlayerEntry { Name = "OSTON URUNOV", Path = "oston_urunov.jpg" },
            new PlayerEntry { Name = "OSWIN APPOLLIS", Path = "oswin_appollis.jpg" },
            new PlayerEntry { Name = "OTABEK SHUKUROV", Path = "otabek_shukurov.jpg" },
            new PlayerEntry { Name = "OTE DIVOIRE", Path = "ote_divoire.jpg" },
            new PlayerEntry { Name = "OUMAR DIAKITE", Path = "oumar_diakite.jpg" },
            new PlayerEntry { Name = "OUSMANE DEMBELE", Path = "ousmane_dembele.jpg" },
            new PlayerEntry { Name = "OUSMANE DIOMANDE", Path = "ousmane_diomande.jpg" },
            new PlayerEntry { Name = "PA NA MA", Path = "pa_na_ma.jpg" },
            new PlayerEntry { Name = "PAPE MATAR SARR", Path = "pape_matar_sarr.jpg" },
            new PlayerEntry { Name = "PATRICK ANDRADE", Path = "patrick_andrade.jpg" },
            new PlayerEntry { Name = "PATRICK BERG", Path = "patrick_berg.jpg" },
            new PlayerEntry { Name = "PATRICK PENTZ", Path = "patrick_pentz.jpg" },
            new PlayerEntry { Name = "PATRICK WIMMER", Path = "patrick_wimmer.jpg" },
            new PlayerEntry { Name = "PATRIK SCHICK", Path = "patrik_schick.jpg" },
            new PlayerEntry { Name = "PAVEL SULC", Path = "pavel_sulc.jpg" },
            new PlayerEntry { Name = "PEDRI", Path = "pedri.jpg" },
            new PlayerEntry { Name = "PEDRO MIGUEL", Path = "pedro_miguel.jpg" },
            new PlayerEntry { Name = "PEDRO NETO", Path = "pedro_neto.jpg" },
            new PlayerEntry { Name = "PEDRO PORRO", Path = "pedro_porro.jpg" },
            new PlayerEntry { Name = "PEDRO VITE", Path = "pedro_vite.jpg" },
            new PlayerEntry { Name = "PERVIS ESTUPINAN", Path = "pervis_estupinan.jpg" },
            new PlayerEntry { Name = "PHIL FODEN", Path = "phil_foden.jpg" },
            new PlayerEntry { Name = "PHILIPP LIENHART", Path = "philipp_lienhart.jpg" },
            new PlayerEntry { Name = "PHILIPP MWENE", Path = "philipp_mwene.jpg" },
            new PlayerEntry { Name = "PICO", Path = "pico.jpg" },
            new PlayerEntry { Name = "PIERO HINCAPIE", Path = "piero_hincapie.jpg" },
            new PlayerEntry { Name = "PROVOD", Path = "provod.jpg" },
            new PlayerEntry { Name = "RAFAEL LEAO", Path = "rafael_leao.jpg" },
            new PlayerEntry { Name = "RAMIZ ZERROUKI", Path = "ramiz_zerrouki.jpg" },
            new PlayerEntry { Name = "RAMON SOSA", Path = "ramon_sosa.jpg" },
            new PlayerEntry { Name = "RAMV RABIA", Path = "ramv_rabia.jpg" },
            new PlayerEntry { Name = "RAPHINHA", Path = "raphinha.jpg" },
            new PlayerEntry { Name = "RAUL JIMENEZ", Path = "raul_jimenez.jpg" },
            new PlayerEntry { Name = "RAYAN AIT-NOURL", Path = "rayan_ait_nourl.jpg" },
            new PlayerEntry { Name = "REBIN SULAKA", Path = "rebin_sulaka.jpg" },
            new PlayerEntry { Name = "REMO FREULER", Path = "remo_freuler.jpg" },
            new PlayerEntry { Name = "RICARDO ADE", Path = "ricardo_ade.jpg" },
            new PlayerEntry { Name = "RICARDO PEPI", Path = "ricardo_pepi.jpg" },
            new PlayerEntry { Name = "RICARDO RODRIGUEZ", Path = "ricardo_rodriguez.jpg" },
            new PlayerEntry { Name = "RICHARD RIOS", Path = "richard_rios.jpg" },
            new PlayerEntry { Name = "RICHIE LARVEA", Path = "richie_larvea.jpg" },
            new PlayerEntry { Name = "RIDLE BAKU", Path = "ridle_baku.jpg" },
            new PlayerEntry { Name = "RIMAD MAHREZ", Path = "rimad_mahrez.jpg" },
            new PlayerEntry { Name = "ROBERTO ALVARADO", Path = "roberto_alvarado.jpg" },
            new PlayerEntry { Name = "ROBERTO FERNANDEZ", Path = "roberto_fernandez.jpg" },
            new PlayerEntry { Name = "ROBIN LE NORMAND", Path = "robin_le_normand.jpg" },
            new PlayerEntry { Name = "RODRIGO BENTANCUR", Path = "rodrigo_bentancur.jpg" },
            new PlayerEntry { Name = "RODRIGO DE PAUL", Path = "rodrigo_de_paul.jpg" },
            new PlayerEntry { Name = "RODRVGO", Path = "rodrvgo.jpg" },
            new PlayerEntry { Name = "ROMAN SAISS", Path = "roman_saiss.jpg" },
            new PlayerEntry { Name = "ROMANO SCHMID", Path = "romano_schmid.jpg" },
            new PlayerEntry { Name = "ROMELU LUKAKU", Path = "romelu_lukaku.jpg" },
            new PlayerEntry { Name = "ROMERO", Path = "romero.jpg" },
            new PlayerEntry { Name = "RONALD ARAUJO", Path = "ronald_araujo.jpg" },
            new PlayerEntry { Name = "RONWEN WILLIAMS", Path = "ronwen_williams.jpg" },
            new PlayerEntry { Name = "ROONY BARDGHIJI", Path = "roony_bardghiji.jpg" },
            new PlayerEntry { Name = "ROOZBEH CHESHMI", Path = "roozbeh_cheshmi.jpg" },
            new PlayerEntry { Name = "ROSHON VAN EIJMA", Path = "roshon_van_eijma.jpg" },
            new PlayerEntry { Name = "RUBEN DIAS", Path = "ruben_dias.jpg" },
            new PlayerEntry { Name = "RUBEN NEVES", Path = "ruben_neves.jpg" },
            new PlayerEntry { Name = "RUBEN VARGAS", Path = "ruben_vargas.jpg" },
            new PlayerEntry { Name = "RUSTAM ASHURMATOV", Path = "rustam_ashurmatov.jpg" },
            new PlayerEntry { Name = "RYAN CHRISTIE", Path = "ryan_christie.jpg" },
            new PlayerEntry { Name = "RYAN GRAVENBERCH", Path = "ryan_gravenberch.jpg" },
            new PlayerEntry { Name = "RYAN MENDES", Path = "ryan_mendes.jpg" },
            new PlayerEntry { Name = "RYAN THOMAS", Path = "ryan_thomas.jpg" },
            new PlayerEntry { Name = "SAEED EZATOLAHI", Path = "saeed_ezatolahi.jpg" },
            new PlayerEntry { Name = "SAID BENRAHMA", Path = "said_benrahma.jpg" },
            new PlayerEntry { Name = "SALEEM OBAID", Path = "saleem_obaid.jpg" },
            new PlayerEntry { Name = "SALEH ABU ALSHAMAT", Path = "saleh_abu_alshamat.jpg" },
            new PlayerEntry { Name = "SALEH ALSHEHRI", Path = "saleh_alshehri.jpg" },
            new PlayerEntry { Name = "SALEH HARDANI", Path = "saleh_hardani.jpg" },
            new PlayerEntry { Name = "SALEM ALDAWSARI", Path = "salem_aldawsari.jpg" },
            new PlayerEntry { Name = "SALIS ABDUL SAMED", Path = "salis_abdul_samed.jpg" },
            new PlayerEntry { Name = "SAMAN GHODDOS", Path = "saman_ghoddos.jpg" },
            new PlayerEntry { Name = "SAMED BADAR", Path = "samed_badar.jpg" },
            new PlayerEntry { Name = "SAMUEL ADEKUGBE", Path = "samuel_adekugbe.jpg" },
            new PlayerEntry { Name = "SAMUEL MOUTOUSSAMV", Path = "samuel_moutoussamv.jpg" },
            new PlayerEntry { Name = "SAMUKELE KABINI", Path = "samukele_kabini.jpg" },
            new PlayerEntry { Name = "SANDER BERGE", Path = "sander_berge.jpg" },
            new PlayerEntry { Name = "SANTIAGO ARIAS", Path = "santiago_arias.jpg" },
            new PlayerEntry { Name = "SANTIAGO GIMENEZ", Path = "santiago_gimenez.jpg" },
            new PlayerEntry { Name = "SANTIAGO MIELE", Path = "santiago_miele.jpg" },
            new PlayerEntry { Name = "SARDAR AZMOUN", Path = "sardar_azmoun.jpg" },
            new PlayerEntry { Name = "SAUD ABDULHAMID", Path = "saud_abdulhamid.jpg" },
            new PlayerEntry { Name = "SAVFALLAH LTAIEF", Path = "savfallah_ltaief.jpg" },
            new PlayerEntry { Name = "SCOTT MCKENNA", Path = "scott_mckenna.jpg" },
            new PlayerEntry { Name = "SCOTT MCTOMLNAV", Path = "scott_mctomlnav.jpg" },
            new PlayerEntry { Name = "SEAD KOLASINAC", Path = "sead_kolasinac.jpg" },
            new PlayerEntry { Name = "SEBASTIAN CACERES", Path = "sebastian_caceres.jpg" },
            new PlayerEntry { Name = "SEBASTIEN HAUER", Path = "sebastien_hauer.jpg" },
            new PlayerEntry { Name = "SEIKO FOFANA", Path = "seiko_fofana.jpg" },
            new PlayerEntry { Name = "SERGE GNABRV", Path = "serge_gnabrv.jpg" },
            new PlayerEntry { Name = "SERGIO ROCHET", Path = "sergio_rochet.jpg" },
            new PlayerEntry { Name = "SEUNGGW KIM", Path = "seunggw_kim.jpg" },
            new PlayerEntry { Name = "SEUNGHO PAIK", Path = "seungho_paik.jpg" },
            new PlayerEntry { Name = "SHEREL FLORANUS", Path = "sherel_floranus.jpg" },
            new PlayerEntry { Name = "SHERZOD NASRULLAEV", Path = "sherzod_nasrullaev.jpg" },
            new PlayerEntry { Name = "SHOGO TANIGUCKI", Path = "shogo_tanigucki.jpg" },
            new PlayerEntry { Name = "SHOJAE KHALILZADEH", Path = "shojae_khalilzadeh.jpg" },
            new PlayerEntry { Name = "SHURANDV SAMBO", Path = "shurandv_sambo.jpg" },
            new PlayerEntry { Name = "SHUTO MACHINO", Path = "shuto_machino.jpg" },
            new PlayerEntry { Name = "SILVAN WIDMER", Path = "silvan_widmer.jpg" },
            new PlayerEntry { Name = "SIMON ADINGRA", Path = "simon_adingra.jpg" },
            new PlayerEntry { Name = "SIPHO CHAINE", Path = "sipho_chaine.jpg" },
            new PlayerEntry { Name = "SIPHO MBULE", Path = "sipho_mbule.jpg" },
            new PlayerEntry { Name = "SIVABONGA NGEZANA", Path = "sivabonga_ngezana.jpg" },
            new PlayerEntry { Name = "SOFVAN AMRABAT", Path = "sofvan_amrabat.jpg" },
            new PlayerEntry { Name = "SONTJE HANSEN", Path = "sontje_hansen.jpg" },
            new PlayerEntry { Name = "STEFAN POSCH", Path = "stefan_posch.jpg" },
            new PlayerEntry { Name = "STEPHEN EUSTAQUIO", Path = "stephen_eustaquio.jpg" },
            new PlayerEntry { Name = "STEVEN MOREIRA", Path = "steven_moreira.jpg" },
            new PlayerEntry { Name = "SULTAN ALBRAKE", Path = "sultan_albrake.jpg" },
            new PlayerEntry { Name = "TABAKOVIC", Path = "tabakovic.jpg" },
            new PlayerEntry { Name = "TAJON BUCHANAN", Path = "tajon_buchanan.jpg" },
            new PlayerEntry { Name = "TAKEFUSA KUBO", Path = "takefusa_kubo.jpg" },
            new PlayerEntry { Name = "TAKUMI MINAMINO", Path = "takumi_minamino.jpg" },
            new PlayerEntry { Name = "TANNER TESSMANN", Path = "tanner_tessmann.jpg" },
            new PlayerEntry { Name = "TAREK SALMAN", Path = "tarek_salman.jpg" },
            new PlayerEntry { Name = "TARIK MUHAREMOVIC", Path = "tarik_muharemovic.jpg" },
            new PlayerEntry { Name = "TARIO LAMPTEV", Path = "tario_lamptev.jpg" },
            new PlayerEntry { Name = "TEUN KOOPMEINERS", Path = "teun_koopmeiners.jpg" },
            new PlayerEntry { Name = "THALENTE MBATHA", Path = "thalente_mbatha.jpg" },
            new PlayerEntry { Name = "THEO BONGONDA", Path = "theo_bongonda.jpg" },
            new PlayerEntry { Name = "THEO HERNANDEZ", Path = "theo_hernandez.jpg" },
            new PlayerEntry { Name = "THOMAS MEUNIER", Path = "thomas_meunier.jpg" },
            new PlayerEntry { Name = "THOMAS PARTEV", Path = "thomas_partev.jpg" },
            new PlayerEntry { Name = "TIELEMANS", Path = "tielemans.jpg" },
            new PlayerEntry { Name = "TIJJANI REIJNDERS", Path = "tijjani_reijnders.jpg" },
            new PlayerEntry { Name = "TIM PAVNE", Path = "tim_pavne.jpg" },
            new PlayerEntry { Name = "TIMOTHY WEAH", Path = "timothy_weah.jpg" },
            new PlayerEntry { Name = "TOMAS CHORV", Path = "tomas_chorv.jpg" },
            new PlayerEntry { Name = "TOMAS HOLES", Path = "tomas_holes.jpg" },
            new PlayerEntry { Name = "TORBJORN HEGGEM", Path = "torbjorn_heggem.jpg" },
            new PlayerEntry { Name = "TREZEGUET", Path = "trezeguet.jpg" },
            new PlayerEntry { Name = "TSUYOSHI WATANABE", Path = "tsuyoshi_watanabe.jpg" },
            new PlayerEntry { Name = "TYLER ADAMS", Path = "tyler_adams.jpg" },
            new PlayerEntry { Name = "UGURCAN CAKIR", Path = "ugurcan_cakir.jpg" },
            new PlayerEntry { Name = "UMAR ESHMURODOV", Path = "umar_eshmurodov.jpg" },
            new PlayerEntry { Name = "UNAI SIMON", Path = "unai_simon.jpg" },
            new PlayerEntry { Name = "USA", Path = "usa.jpg" },
            new PlayerEntry { Name = "UZBEKISTAN FOOTBALL", Path = "uzbekistan_football.jpg" },
            new PlayerEntry { Name = "VACLAV CERNV", Path = "vaclav_cernv.jpg" },
            new PlayerEntry { Name = "VACLAV CERNV_2", Path = "vaclav_cernv_2.jpg" },
            new PlayerEntry { Name = "VAHIA FOFANA", Path = "vahia_fofana.jpg" },
            new PlayerEntry { Name = "VAN DIOMANDE", Path = "van_diomande.jpg" },
            new PlayerEntry { Name = "VAN DIOMANDE_2", Path = "van_diomande_2.jpg" },
            new PlayerEntry { Name = "VAN VALERV", Path = "van_valerv.jpg" },
            new PlayerEntry { Name = "VANNICK SEMEDO", Path = "vannick_semedo.jpg" },
            new PlayerEntry { Name = "VASIL KUSEJ", Path = "vasil_kusej.jpg" },
            new PlayerEntry { Name = "VASIN AVARI", Path = "vasin_avari.jpg" },
            new PlayerEntry { Name = "VAZAN AL-ARAB", Path = "vazan_al_arab.jpg" },
            new PlayerEntry { Name = "VAZAN AL-NAIMAT", Path = "vazan_al_naimat.jpg" },
            new PlayerEntry { Name = "VAZEED ABULAILA", Path = "vazeed_abulaila.jpg" },
            new PlayerEntry { Name = "VERRV MINA", Path = "verrv_mina.jpg" },
            new PlayerEntry { Name = "VICTOR NILSSON LINDELBF", Path = "victor_nilsson_lindelbf.jpg" },
            new PlayerEntry { Name = "VIJKI SOMA", Path = "vijki_soma.jpg" },
            new PlayerEntry { Name = "VIKTOR JOHANSSON", Path = "viktor_johansson.jpg" },
            new PlayerEntry { Name = "VINICIUS JUNIOR", Path = "vinicius_junior.jpg" },
            new PlayerEntry { Name = "VIRGIL VAN DIJK", Path = "virgil_van_dijk.jpg" },
            new PlayerEntry { Name = "VITINHA", Path = "vitinha.jpg" },
            new PlayerEntry { Name = "VOANE WISSA", Path = "voane_wissa.jpg" },
            new PlayerEntry { Name = "VOUCEF ATAL", Path = "voucef_atal.jpg" },
            new PlayerEntry { Name = "VOUNGWOO SEOL", Path = "voungwoo_seol.jpg" },
            new PlayerEntry { Name = "VOUSSEF AMVN", Path = "voussef_amvn.jpg" },
            new PlayerEntry { Name = "VOZINHA", Path = "vozinha.jpg" },
            new PlayerEntry { Name = "VUNUS AKGUN", Path = "vunus_akgun.jpg" },
            new PlayerEntry { Name = "WAGNER PINA", Path = "wagner_pina.jpg" },
            new PlayerEntry { Name = "WESLEY", Path = "wesley.jpg" },
            new PlayerEntry { Name = "WESTON MCKENNIE", Path = "weston_mckennie.jpg" },
            new PlayerEntry { Name = "WILFRIED SINGO", Path = "wilfried_singo.jpg" },
            new PlayerEntry { Name = "WILLIAM SAUBA", Path = "william_sauba.jpg" },
            new PlayerEntry { Name = "WILLIAN PACHO", Path = "willian_pacho.jpg" },
            new PlayerEntry { Name = "WILLV BOLV", Path = "willv_bolv.jpg" },
            new PlayerEntry { Name = "WILLY SEMEDO", Path = "willy_semedo.jpg" },
            new PlayerEntry { Name = "WON MVOGO", Path = "won_mvogo.jpg" },
            new PlayerEntry { Name = "WOUT WEGHORST", Path = "wout_weghorst.jpg" },
            new PlayerEntry { Name = "XAVER SCHLAGER", Path = "xaver_schlager.jpg" },
            new PlayerEntry { Name = "XAVI SIMONS", Path = "xavi_simons.jpg" },
            new PlayerEntry { Name = "YASSINE BOUNOU", Path = "yassine_bounou.jpg" },
            new PlayerEntry { Name = "YASSINE MERIAH", Path = "yassine_meriah.jpg" },
            new PlayerEntry { Name = "YOUSSEF EN-NESVRI", Path = "youssef_en_nesvri.jpg" },
            new PlayerEntry { Name = "YUMIN CHO", Path = "yumin_cho.jpg" },
            new PlayerEntry { Name = "ZAID TAHSEEN", Path = "zaid_tahseen.jpg" },
            new PlayerEntry { Name = "ZEKI AMDOUNI", Path = "zeki_amdouni.jpg" },
            new PlayerEntry { Name = "ZEKI CELIK", Path = "zeki_celik.jpg" },
            new PlayerEntry { Name = "ZENO DEBAST", Path = "zeno_debast.jpg" },
            new PlayerEntry { Name = "ZIDANE IOBAL", Path = "zidane_iobal.jpg" },
            new PlayerEntry { Name = "ZIYAD ALJOHANI", Path = "ziyad_aljohani.jpg" }
        };

        public FigurinhaController()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "AlbumCopa2026.db3");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Figurinha>();
        }

        public List<Figurinha> ListarTodos()
        {
            return _database.Table<Figurinha>().ToList();
        }

        public (bool Sucesso, string Mensagem) SalvarFigurinha(Figurinha figurinha)
        {
            if (string.IsNullOrWhiteSpace(figurinha.NomeJogador))
                return (false, "O nome do jogador é obrigatório.");

            try
            {
                if (figurinha.Id != 0)
                {
                    _database.Update(figurinha);
                    return (true, "Figurinha atualizada com sucesso!");
                }
                else
                {
                    // verifica se já existe um jogador com este nome e seleção no banco para evitar duplicatas exatas
                    var existente = _database.Table<Figurinha>()
                                             .FirstOrDefault(f => f.NomeJogador == figurinha.NomeJogador);

                    if (existente != null)
                    {
                        existente.Quantidade++;
                        existente.Obtido = true;
                        _database.Update(existente);
                        return (true, "Você já possuía este jogador! Uma cópia extra foi adicionada como repetida.");
                    }
                    else
                    {
                        figurinha.Quantidade = 1;
                        figurinha.NoAlbum = false;
                        _database.Insert(figurinha);
                        return (true, "Figurinha cadastrada com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao salvar no banco de dados: {ex.Message}");
            }
        }

        public (bool Sucesso, string Mensagem) ExcluirFigurinha(Figurinha figurinha)
        {
            try
            {
                _database.Delete(figurinha);
                return (true, "Figurinha excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao excluir: {ex.Message}");
            }
        }

        public void AlternarStatusObtido(Figurinha f)
        {
            f.Obtido = !f.Obtido;
            _database.Update(f);
        }



        public void ColarNoAlbum(Figurinha f)
        {
            f.NoAlbum = true;
            f.Obtido = true;
            _database.Update(f);
        }

        public int ColarTodasAdquiridas()
        {
            var obtidasNaoColadas = _database.Table<Figurinha>()
                                            .Where(f => f.Obtido && !f.NoAlbum)
                                            .ToList();
            foreach (var f in obtidasNaoColadas)
            {
                f.NoAlbum = true;
                _database.Update(f);
            }
            return obtidasNaoColadas.Count;
        }

        public void ColarListaNoAlbum(List<Figurinha> lista)
        {
            foreach (var f in lista)
            {
                var existente = _database.Table<Figurinha>().FirstOrDefault(x => x.NomeJogador == f.NomeJogador);
                if (existente != null)
                {
                    existente.NoAlbum = true;
                    existente.Obtido = true;
                    _database.Update(existente);
                }
            }
        }

        public List<Figurinha> ListarFigurinhas(string busca, bool? apenasObtidos, bool? apenasDesejados)
        {
            var query = _database.Table<Figurinha>();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                var buscaLower = busca.ToLower().Trim();
                query = query.Where(f => f.NomeJogador.ToLower().Contains(buscaLower) || f.Selecao.ToLower().Contains(buscaLower));
            }

            var list = query.ToList();

            if (apenasObtidos == true)
            {
                list = list.Where(f => f.Obtido).ToList();
            }

            if (apenasDesejados == true)
            {
                list = list.Where(f => f.Desejado).ToList();
            }

            return list;
        }

        public List<Figurinha> SortearPacotinho(int quantidade = 7)
        {
            var figurinhasSorteadas = new List<Figurinha>();
            if (PoolJogadores.Length == 0)
                return figurinhasSorteadas;

            for (int i = 0; i < quantidade; i++)
            {
                // sorteia um jogador aleatório da lista
                var jogadorSorteado = PoolJogadores[_random.Next(PoolJogadores.Length)];
                
                string selecao = "Não Definida";

                // verifica se o jogador está no sqlite
                var existente = _database.Table<Figurinha>()
                                         .FirstOrDefault(f => f.NomeJogador == jogadorSorteado.Name);

                if (existente != null)
                {
                    if (!existente.Obtido)
                    {
                        existente.Obtido = true;
                        existente.Quantidade = 1;
                    }
                    else
                    {
                        existente.Quantidade++;
                    }
                    _database.Update(existente);
                    figurinhasSorteadas.Add(existente);
                }
                else
                {
                    var nova = new Figurinha
                    {
                        NomeJogador = jogadorSorteado.Name,
                        Selecao = selecao,
                        Tipo = _random.Next(10) == 0 ? "Especial" : "Comum",
                        Obtido = true,
                        Desejado = false,
                        FotoPath = jogadorSorteado.Path,
                        Quantidade = 1,
                        NoAlbum = false
                    };
                    _database.Insert(nova);
                    figurinhasSorteadas.Add(nova);
                }
            }

            return figurinhasSorteadas;
        }

        // lista de paises da copa
        public static readonly string[] ListaSelecoes = new string[]
        {
            "África do Sul", "Alemanha", "Arábia Saudita", "Argélia", "Argentina",
            "Austrália", "Áustria", "Bélgica", "Bósnia e Herzegovina", "Brasil",
            "Canadá", "Colômbia", "Coreia do Sul", "Costa do Marfim", "Croácia",
            "Curaçau", "Equador", "Escócia", "Espanha", "Estados Unidos",
            "França", "Gana", "Haiti", "Holanda", "Inglaterra",
            "Irã", "Iraque", "Japão", "Jordânia", "México",
            "Noruega", "Nova Zelândia", "Panamá", "Paraguai", "Portugal",
            "Qatar", "RD Congo", "República Tcheca", "Suécia", "Suíça",
            "Tunísia", "Turquia", "Uruguai", "Uzbequistão"
        };


    }
}
