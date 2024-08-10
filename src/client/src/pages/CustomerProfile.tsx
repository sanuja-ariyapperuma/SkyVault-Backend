import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";
import Typography from "@mui/material/Typography";
import ArrowDownwardIcon from "@mui/icons-material/ArrowDropUp";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import axios from "axios";
import { FormControlLabel, Radio, RadioGroup } from "@mui/material";

import localStyles from "../components/CreateProfile/CustomerProfile.module.css";
import { baseURL } from "../features/services/apiCalls";
import { ProfileDefinitionResponse } from "../features/Types/CustomerProfile/CommonDefinitions";
import { OptionsType } from "../features/Types/Dashboard/dashboardTypes";
import globalStyles from "../components/CommonComponents/Common.module.css";
import PassportInfo from "../components/CreateProfile/PassportInfo";
import {
  SavePassportRequestType,
  SavePassportResponseType,
} from "../features/Types/CustomerProfile/CustomerProfileType";
import { PassportType } from "../features/Types/CustomerProfile/PassportType";
import ButtonPanel from "../components/CommonComponents/ButtonPanel";
import {
  notifyError,
  notifySuccess,
} from "../components/CommonComponents/Toasters";

const CustomerProfile = () => {
  const { profileId } = useParams();
  const [salutations, setSalutations] = useState<OptionsType[]>([]);
  const [gender, setGender] = useState<OptionsType[]>([]);
  const [nationality, setNationality] = useState<OptionsType[]>([]);
  const [country, setCountry] = useState<OptionsType[]>([]);
  const [secondaryPassportEnabled, setSecondaryPassportEnabled] =
    useState<boolean>(true);

  const [customerProfileId, setCustomerProfileId] = useState<string | null>(
    null
  );

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

  const handleFieldChange = (field: string, value: any) => {
    const alphaCheckRegex = /^[a-zA-Z\s]*$/;
    const passportRegex = /^[A-Za-z0-9]*$/;

    if (field === "lastName" && !alphaCheckRegex.test(value)) {
      notifyError("Last name can only contain letters");
      return;
    }

    if (field === "otherNames" && !alphaCheckRegex.test(value)) {
      notifyError("Other names can only contain letters");
      return;
    }

    if (field === "placeOfBirth" && !alphaCheckRegex.test(value)) {
      notifyError("Place of birth can only contain letters");
      return;
    }

    if (field === "passportNumber" && !passportRegex.test(value)) {
      notifyError(`Passport number can only contain letters and numbers`);
      return;
    }

    setPrimaryPassport({
      ...primaryPassport,
      [field]: value.toString().trim(),
    });
  };

  const handleFieldChangeSecondaryPassport = (field: string, value: any) => {
    setSecondaryPassport({
      ...secondaryPassport,
      [field]: value,
    });
  };

  const handleOnSecondaryPassportAddRemove = () => {
    if (secondaryPassportEnabled) {
      setSecondaryPassport({
        id: "",
        salutationId: primaryPassport.salutationId,
        lastName: primaryPassport.lastName,
        otherNames: primaryPassport.otherNames,
        nationalityId: primaryPassport.nationalityId,
        genderId: primaryPassport.genderId,
        placeOfBirth: primaryPassport.placeOfBirth,
        passportNumber: "",
        dateOfBirth: primaryPassport.dateOfBirth,
        passportExpiryDate: null,
        countryId: primaryPassport.countryId,
      });
      setSecondaryPassportEnabled(!secondaryPassportEnabled);
    }
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

    if (profileId) {
      //fetch customer profile
    } else {
      //setCustomerProfile(initialCustomerProfile);
    }
  }, []);

  const validatePassport = (passport: SavePassportRequestType): boolean => {
    let valid = true;
    if (!passport.CountryId) {
      notifyError("Country is required");
      valid = false;
    }

    if (!passport.DateOfBirth) {
      notifyError("Date of birth required");
      valid = false;
    }

    if (!passport.ExpiryDate) {
      notifyError("Passport expiry date is required");
      valid = false;
    }

    if (!passport.Gender) {
      notifyError("Gender is required");
      valid = false;
    }

    if (!passport.LastName) {
      notifyError("Last name is required");
      valid = false;
    }

    if (!passport.NationalityId) {
      notifyError("Nationality is required");
      valid = false;
    }

    if (!passport.OtherNames) {
      notifyError("Other names is required");
      valid = false;
    }

    if (!passport.PassportNumber) {
      notifyError("Passport number is required");
      valid = false;
    }

    if (!/^[A-Za-z0-9]{6,9}$/.test(passport.PassportNumber)) {
      notifyError("Invalid passport number");
      valid = false;
    }

    if (!passport.PlaceOfBirth) {
      notifyError("Place of birth is required");
      valid = false;
    }

    if (!passport.SalutationId) {
      notifyError("Salutation is required");
      valid = false;
    }

    return valid;
  };

  const savePassport = (passport: SavePassportRequestType): void => {
    const isValid = validatePassport(passport);

    if (!isValid) {
      return;
    }

    axios
      .post<SavePassportResponseType>(`${baseURL}/AddPassport`, passport)
      .then((response) => {
        console.log("Response", response);
        console.log("Response data", response.data);
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

  const handleOnSavePrimaryPassport = (): void => {
    const passport: SavePassportRequestType = {
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

    savePassport(passport);
  };

  const handleOnSaveSecondaryPassport = (): void => {
    if (!customerProfileId) {
      notifyError("Primary passport must be saved first");
      return;
    }

    const passport: SavePassportRequestType = {
      Id: null,
      CustomerProfileId: customerProfileId,
      SystemUserId: "9",
      ParentId: "",
      LastName: secondaryPassport.lastName,
      OtherNames: secondaryPassport.otherNames,
      PassportNumber: secondaryPassport.passportNumber,
      Gender: secondaryPassport.genderId,
      DateOfBirth: secondaryPassport.dateOfBirth?.toString() ?? "",
      PlaceOfBirth: secondaryPassport.placeOfBirth,
      ExpiryDate: secondaryPassport.passportExpiryDate?.toString() ?? "",
      NationalityId: secondaryPassport.nationalityId,
      CountryId: secondaryPassport.countryId,
      IsPrimary: "0",
      SalutationId: secondaryPassport.salutationId,
    };

    savePassport(passport);
  };

  return (
    <div className={localStyles.profileManagementContainer}>
      <div className={localStyles.topHeading}>
        <h2 className={localStyles.pageHeader}>
          {profileId ? "Update Profile" : "Create Profile"}
        </h2>
      </div>
      <Accordion>
        <AccordionSummary
          expandIcon={<ArrowDownwardIcon />}
          aria-controls="panel1-content"
          id="panel1-header"
        >
          <Typography>
            <span className={localStyles.accordionHeader}>
              Primary Passport Information
            </span>
          </Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            <div className={localStyles.buttonContainer}>
              {primaryPassport.id && !secondaryPassport.id && (
                <button
                  className={
                    secondaryPassportEnabled
                      ? globalStyles.customButton
                      : globalStyles.customButtonDanger
                  }
                  onClick={handleOnSecondaryPassportAddRemove}
                >
                  {/* {secondaryPassportEnabled
                    ? "Add Secondary"
                    : "Remove Secondary"} */}
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
                <div className={localStyles.secondaryPassportArea}>
                  <b>Secondary Passport Information</b>
                  <div className={localStyles.buttonContainer}>
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
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary
          expandIcon={<ArrowDropDownIcon />}
          aria-controls="panel2-content"
          id="panel2-header"
        >
          <Typography>
            <span className={localStyles.accordionHeader}>
              VISA Information (Optional)
            </span>
          </Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            {/* <VISAInfo
              country={country}
              handleOnDateOfBirthChange={handleOnDateOfBirthChange}
              handleOnCountrySelect={handleOnCountrySelect}
            /> */}
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary
          expandIcon={<ArrowDropDownIcon />}
          aria-controls="panel2-content"
          id="panel2-header"
        >
          <Typography>
            <span className={localStyles.accordionHeader}>
              Frequent Flyer (Optional)
            </span>
          </Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            <div className={localStyles.accordionContent}>
              <div className={localStyles.accordionLeft}>
                <input
                  type="text"
                  placeholder="Frequent Flyer Number"
                  className={globalStyles.commonTextInput}
                />
              </div>
            </div>
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary
          expandIcon={<ArrowDropDownIcon />}
          aria-controls="panel2-content"
          id="panel2-header"
        >
          <Typography>
            <span className={localStyles.accordionHeader}>
              Preferred Communication Method
            </span>
          </Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            <div className={localStyles.accordionContent}>
              <RadioGroup
                defaultValue="None"
                name="radio-buttons-group"
                sx={{ display: "flex", flexDirection: "row" }}
              >
                <FormControlLabel
                  value="None"
                  control={<Radio />}
                  label="None"
                />
                <FormControlLabel
                  value="WhatsApp"
                  control={<Radio />}
                  label="WhatsApp"
                />
                <FormControlLabel
                  value="Email"
                  control={<Radio />}
                  label="Email"
                />
              </RadioGroup>
            </div>
          </Typography>
        </AccordionDetails>
      </Accordion>
      {/* <div className={localStyles.footerButtonArea}>
        <ButtonPanel OnSave={handleOnSavePrimaryPassport} />
      </div> */}
    </div>
  );
};

export default CustomerProfile;
