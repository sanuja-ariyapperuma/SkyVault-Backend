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

import {
  PassportType,
  SavePassportRequestType,
  SavePassportResponseType,
} from "../../features/Types/CustomerProfile/PassportType";

import {
  convertPassportTypeToSavePassportRequestType,
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
  setPrimaryPassportSavedId: (id: string) => void;
  setSecondaryPassportSavedId: (id: string) => void;
};

const PassportAccordion = (props: PassportAccordionProps) => {
  const {
    setCustomerProfileId,
    customerProfileId,
    country,
    setCountry,
    setPrimaryPassportSavedId,
    setSecondaryPassportSavedId,
  } = props;
  const [salutations, setSalutations] = useState<OptionsType[]>([]);
  const [gender, setGender] = useState<OptionsType[]>([]);
  const [nationality, setNationality] = useState<OptionsType[]>([]);

  useEffect(() => {
    if (customerProfileId) {
      getPassportData();
    }
  }, [customerProfileId]);

  const initialPassportData = {
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
  };

  const [primaryPassport, setPrimaryPassport] = useState<PassportType>({
    ...initialPassportData,
    isPrimary: "1",
  });

  const [secondaryPassport, setSecondaryPassport] = useState<PassportType>({
    ...initialPassportData,
    isPrimary: "0",
  });

  const [secondaryPassportEnabled, setSecondaryPassportEnabled] =
    useState<boolean>(true);

  const getPassportData = () => {
    axios
      .post<PassportType[] | null>(`${baseURL}/getPassportsByCustomerId`, {
        CustomerProfileId: customerProfileId,
        SystemUserId: "9",
      })
      .then((response) => {
        response.data?.map((passport) => {
          if (passport.isPrimary === "1") {
            setPrimaryPassport({
              ...passport,
              passportExpiryDate: passport.passportExpiryDate
                ? new Date(passport.passportExpiryDate)
                : null,
              dateOfBirth: passport.dateOfBirth
                ? new Date(passport.dateOfBirth)
                : null,
            });
            setPrimaryPassportSavedId(passport.id);
          }

          if (passport.isPrimary === "0") {
            setSecondaryPassport({
              ...passport,
              passportExpiryDate: passport.passportExpiryDate
                ? new Date(passport.passportExpiryDate)
                : null,
              dateOfBirth: passport.dateOfBirth
                ? new Date(passport.dateOfBirth)
                : null,
            });
            setSecondaryPassportSavedId(passport.id);
          }
        });
      })
      .catch((error) => {
        console.log("Error", error);
      });
  };

  const handleOnSecondaryPassportAddRemove = () => {
    if (secondaryPassportEnabled) {
      setSecondaryPassport({
        ...primaryPassport,
        isPrimary: "0",
        id: "",
        passportNumber: "",
        passportExpiryDate: null,
      });
      setSecondaryPassportEnabled(!secondaryPassportEnabled);
    }
  };

  const handleOnSavePrimaryPassport = (): void => {
    const passport: SavePassportRequestType =
      convertPassportTypeToSavePassportRequestType(primaryPassport);
    passport.CustomerProfileId = customerProfileId;
    savePassport(passport);
  };

  const savePassport = (passport: SavePassportRequestType): void => {
    const isValid = validatePassport(passport);

    if (!isValid) return;

    const url = passport.Id
      ? `${baseURL}/UpdatePassport`
      : `${baseURL}/AddPassport`;

    axios
      .post<SavePassportResponseType>(url, passport)
      .then((response) => {
        if (passport.Id) {
          notifySuccess("Passport updated successfully");
          return;
        }

        const responseData = response.data;

        if (passport.IsPrimary === "1") {
          setCustomerProfileId(responseData.customerProfileId);
          setPrimaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
          setPrimaryPassportSavedId(responseData.passportId);
        }

        if (passport.IsPrimary === "0") {
          setSecondaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
          setSecondaryPassportSavedId(responseData.passportId);
        }

        notifySuccess("Passport added successfully");
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

    const passport: SavePassportRequestType =
      convertPassportTypeToSavePassportRequestType(secondaryPassport);
    passport.CustomerProfileId = customerProfileId;
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
          {(!secondaryPassportEnabled || secondaryPassport.id) && (
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
