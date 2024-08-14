import { useEffect, useState } from "react";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Typography,
} from "@mui/material";
import ArrowDownwardIcon from "@mui/icons-material/ArrowDropUp";
import ButtonPanel from "../CommonComponents/ButtonPanel";
import PassportInfo from "./PassportInfo";

import customerProfileStyles from "./CustomerProfile.module.css";
import globalStyles from "./../CommonComponents/Common.module.css";

import { PassportType } from "../../features/Types/CustomerProfile/PassportType";
import {
  SavePassportRequestType,
  SavePassportResponseType,
} from "../../features/Types/CustomerProfile/CustomerProfileType";
import {
  validatePassport,
  validatePassportFields,
} from "../../features/Helpers/helper";
import axios from "axios";
import { baseURL } from "../../features/services/apiCalls";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import { ProfileDefinitionResponse } from "../../features/Types/CustomerProfile/CommonDefinitions";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";

type PassportAccordionProps = {
  setCustomerProfileId: (id: string) => void;
  customerProfileId: string;
  setCountry: (country: OptionsType[]) => void;
  country: OptionsType[];
};

const PassportAccordion = (props: PassportAccordionProps) => {
  const { setCustomerProfileId, customerProfileId, country, setCountry } =
    props;
  const [salutations, setSalutations] = useState<OptionsType[]>([]);
  const [gender, setGender] = useState<OptionsType[]>([]);
  const [nationality, setNationality] = useState<OptionsType[]>([]);
  //const [country, setCountry] = useState<OptionsType[]>([]);

  const [primaryPassport, setPrimaryPassport] = useState<PassportType>({
    id: "",
    salutationId: "",
    lastName: "",
    otherNames: "",
    nationalityId: "",
    genderId: "",
    placeOfBirth: "",
    passportNumber: "",
    dateOfBirth: null,
    passportExpiryDate: null,
    countryId: "",
  });

  const initialPassportDate = {
    Id: null,
    CustomerProfileId: null,
    SystemUserId: "9",
    ParentId: "",
    LastName: primaryPassport.lastName,
    OtherNames: primaryPassport.otherNames,
    PassportNumber: primaryPassport.passportNumber,
    Gender: primaryPassport.genderId,
    DateOfBirth: primaryPassport.dateOfBirth?.toString() ?? "",
    PlaceOfBirth: primaryPassport.placeOfBirth,
    ExpiryDate: primaryPassport.passportExpiryDate?.toString() ?? "",
    NationalityId: primaryPassport.nationalityId,
    CountryId: primaryPassport.countryId,
    IsPrimary: "1",
    SalutationId: primaryPassport.salutationId,
  };

  const [secondaryPassport, setSecondaryPassport] = useState<PassportType>({
    id: "",
    salutationId: "",
    lastName: "",
    otherNames: "",
    nationalityId: "",
    genderId: "",
    placeOfBirth: "",
    passportNumber: "",
    dateOfBirth: null,
    passportExpiryDate: null,
    countryId: "",
  });

  const [secondaryPassportEnabled, setSecondaryPassportEnabled] =
    useState<boolean>(true);

  const handleOnSecondaryPassportAddRemove = () => {
    if (secondaryPassportEnabled) {
      setSecondaryPassport({
        ...primaryPassport,
        id: "",
        passportNumber: "",
        passportExpiryDate: null,
      });
      setSecondaryPassportEnabled(!secondaryPassportEnabled);
    }
  };

  const handleOnSavePrimaryPassport = (): void => {
    const passport: SavePassportRequestType = initialPassportDate;
    savePassport(passport);
  };

  const savePassport = (passport: SavePassportRequestType): void => {
    const isValid = validatePassport(passport);

    if (!isValid) {
      return;
    }

    axios
      .post<SavePassportResponseType>(`${baseURL}/AddPassport`, passport)
      .then((response) => {
        const responseData = response.data;

        if (passport.IsPrimary === "1") {
          setCustomerProfileId(responseData.customerProfileId);
          setPrimaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
        }

        if (passport.IsPrimary === "0") {
          setSecondaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
        }

        notifySuccess("Passport added successfully");
        console.log("Response", response);
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };

  const handleFieldChangeSecondaryPassport = (field: string, value: any) => {
    setSecondaryPassport({
      ...secondaryPassport,
      [field]: value,
    });
  };

  const handleFieldChange = (field: string, value: string) => {
    if (!validatePassportFields(field, value)) return;

    setPrimaryPassport({
      ...primaryPassport,
      [field]: value.toString().trim(),
    });
  };

  const handleOnSaveSecondaryPassport = (): void => {
    if (!customerProfileId) {
      notifyError("Primary passport must be saved first");
      return;
    }

    const passport: SavePassportRequestType = initialPassportDate;
    savePassport(passport);
  };

  const fetchCommonData = async () => {
    try {
      const definitionData = await axios.post<ProfileDefinitionResponse>(
        `${baseURL}/profilepage-commondata`,
        {
          headers: {
            ContentType: "application/json",
          },
        }
      );

      const sal = definitionData.data.salutations.map(
        (salutation): OptionsType => {
          return {
            value: salutation.id,
            label: salutation.name,
          };
        }
      );

      const gend = definitionData.data.genders.map((gender): OptionsType => {
        return {
          value: gender.id,
          label: gender.name,
        };
      });

      const nat = definitionData.data.nationalities.map(
        (natio): OptionsType => {
          return {
            value: natio.id,
            label: natio.name,
          };
        }
      );

      const cntry = definitionData.data.countries.map((cnty): OptionsType => {
        return {
          value: cnty.id,
          label: cnty.name,
        };
      });

      setCountry(cntry);
      setNationality(nat);
      setSalutations(sal);
      setGender(gend);
    } catch (error) {
      console.log("Error", error);
    }
  };

  useEffect(() => {
    fetchCommonData();
  }, []);

  return (
    <Accordion>
      <AccordionSummary
        expandIcon={<ArrowDownwardIcon />}
        aria-controls="panel1-content"
        id="panel1-header"
      >
        <Typography>
          <span className={customerProfileStyles.accordionHeader}>
            Primary Passport Information
          </span>
        </Typography>
      </AccordionSummary>
      <AccordionDetails>
        <div className={customerProfileStyles.buttonContainer}>
          {primaryPassport.id && !secondaryPassport.id && (
            <button
              className={globalStyles.customButton}
              onClick={handleOnSecondaryPassportAddRemove}
            >
              Add Secondary
            </button>
          )}
          <ButtonPanel OnSave={handleOnSavePrimaryPassport} />
        </div>

        <PassportInfo
          salutations={salutations}
          gender={gender}
          nationality={nationality}
          country={country}
          customerPassport={primaryPassport}
          handleFieldChange={handleFieldChange}
        />
        <div>
          {!secondaryPassportEnabled && (
            <div className={customerProfileStyles.secondaryPassportArea}>
              <b>Secondary Passport Information</b>
              <div className={customerProfileStyles.buttonContainer}>
                <ButtonPanel OnSave={handleOnSaveSecondaryPassport} />
              </div>
              <PassportInfo
                salutations={salutations}
                gender={gender}
                nationality={nationality}
                country={country}
                customerPassport={secondaryPassport}
                handleFieldChange={handleFieldChangeSecondaryPassport}
              />
            </div>
          )}
        </div>
      </AccordionDetails>
    </Accordion>
  );
};

export default PassportAccordion;
