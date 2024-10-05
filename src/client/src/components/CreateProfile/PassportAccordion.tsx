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
} from "../../features/Types/CustomerProfile/PassportType";

import {
  convertPassportTypeToSavePassportRequestType,
  isPreviousDate,
  validatePassport,
  validatePassportFields,
} from "../../features/Helpers/helper";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import { Dayjs } from "dayjs";
import { useAccessToken } from "../../hooks/useAccessToken";
import { fetchCommonDataAPI } from "../../features/services/CustomerProfile/CommonData/apiMethods";
import {
  getPassportDataAPI,
  saveUpdateCustomerProfileAPI,
} from "../../features/services/CustomerProfile/apiMethods";
import AddIcon from "@mui/icons-material/Add";

type PassportAccordionProps = {
  setCustomerProfileId: (id: string) => void;
  customerProfileId: string;
  setCountry: (country: OptionsType[]) => void;
  country: OptionsType[];
  setPrimaryPassportSavedId: (id: string) => void;
  setSecondaryPassportSavedId: (id: string) => void;
  parentId?: string;
};

const PassportAccordion = (props: PassportAccordionProps) => {
  const accessToken = useAccessToken();

  const {
    setCustomerProfileId,
    customerProfileId,
    country,
    setCountry,
    setPrimaryPassportSavedId,
    setSecondaryPassportSavedId,
    parentId,
  } = props;
  const [salutations, setSalutations] = useState<OptionsType[]>([]);
  const [gender, setGender] = useState<OptionsType[]>([]);
  const [nationality, setNationality] = useState<OptionsType[]>([]);

  const initialPassportData = {
    id: "",
    salutationId: "",
    lastName: "",
    otherNames: "",
    nationalityId: "",
    genderId: "",
    placeOfBirth: "",
    passportNumber: "",
    dateOfBirth: "",
    passportExpiryDate: "",
    countryId: "",
  };

  useEffect(() => {
    setPrimaryPassport({ ...initialPassportData, isPrimary: "1" });
    setSecondaryPassport({ ...initialPassportData, isPrimary: "0" });
    if (customerProfileId) {
      getPassportData();
    }
  }, [customerProfileId]);

  useEffect(() => {
    fetchCommonData();
  }, [accessToken]);

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
    getPassportDataAPI(customerProfileId)
      .then((response) => {
        response?.map((passport) => {
          if (passport.isPrimary === "1") {
            setPrimaryPassport({
              ...passport,
              passportExpiryDate: passport.passportExpiryDate,
              dateOfBirth: passport.dateOfBirth,
            });
            setPrimaryPassportSavedId(passport.id);
          }

          if (passport.isPrimary === "0") {
            setSecondaryPassport({
              ...passport,
              passportExpiryDate: passport.passportExpiryDate,
              dateOfBirth: passport.dateOfBirth,
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
        passportExpiryDate: "",
        countryId: "",
      });
      setSecondaryPassportEnabled(!secondaryPassportEnabled);
    }
  };

  const handleOnSavePrimaryPassport = (): void => {
    primaryPassport.parentId = parentId ?? "";

    const passport: SavePassportRequestType =
      convertPassportTypeToSavePassportRequestType(primaryPassport);
    passport.CustomerProfileId = customerProfileId;

    savePassport(passport);
  };

  const savePassport = (passport: SavePassportRequestType): void => {
    const isValid = validatePassport(passport);

    if (!isValid) return;

    saveUpdateCustomerProfileAPI(!!passport.Id, passport)
      .then((response) => {
        if (passport.Id) {
          notifySuccess("Passport updated successfully");
          return;
        }

        notifySuccess("Passport added successfully");

        const responseData = response;

        if (passport.IsPrimary === "1") {
          setCustomerProfileId(responseData.customerProfileId);
          setPrimaryPassportSavedId(responseData.passportId);
          setPrimaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
        }

        if (passport.IsPrimary === "0") {
          setSecondaryPassportSavedId(responseData.passportId);
          setSecondaryPassport((prevState) => ({
            ...prevState,
            id: responseData.passportId,
          }));
        }
      })
      .catch((error) => {
        notifyError(error.response.data);
        console.log("Error", error.response.data);
      });
  };

  const handleFieldChangeSecondaryPassport = (field: string, value: any) => {
    if (field === "countryId" && value === primaryPassport.countryId) {
      notifyError(
        "Primary and Secondary passport countries cannot be the same"
      );
      return;
    }

    setSecondaryPassport({
      ...secondaryPassport,
      [field]: value,
    });
  };

  const handleFieldChange = (field: string, value: string | Dayjs) => {
    if (!validatePassportFields(field, value)) return;

    if (field === "countryId" && value === primaryPassport.countryId) {
      notifyError(
        "Primary and Secondary passport countries cannot be the same"
      );
      return;
    }

    if (field == "dateOfBirth" && isPreviousDate(value as string)) {
      notifyError("Date of birth cannot be a future date");
    }

    setPrimaryPassport({
      ...primaryPassport,
      [field]: value.toString(),
    });
  };

  const handleOnSaveSecondaryPassport = (): void => {
    if (!customerProfileId) {
      notifyError("Primary passport must be saved before adding VISAs");
      return;
    }

    const passport: SavePassportRequestType =
      convertPassportTypeToSavePassportRequestType(secondaryPassport);
    passport.CustomerProfileId = customerProfileId;
    savePassport(passport);
  };

  const fetchCommonData = async () => {
    try {
      const definitionData = await fetchCommonDataAPI();

      const sal = definitionData.salutations.map((salutation): OptionsType => {
        return {
          value: salutation.id,
          label: salutation.name,
        };
      });

      const gend = definitionData.genders.map((gender): OptionsType => {
        return {
          value: gender.id,
          label: gender.name,
        };
      });

      const nat = definitionData.nationalities.map((natio): OptionsType => {
        return {
          value: natio.id,
          label: natio.name,
        };
      });

      const cntry = definitionData.countries.map((cnty): OptionsType => {
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
      console.log("Error retrieving common data : ", error);
    }
  };

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
              <AddIcon />
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
                isSecondary={true}
              />
            </div>
          )}
        </div>
      </AccordionDetails>
    </Accordion>
  );
};

export default PassportAccordion;
