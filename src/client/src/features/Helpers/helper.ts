import { notifyError } from "../../components/CommonComponents/Toasters";
import {
  PassportType,
  SavePassportRequestType,
} from "../Types/CustomerProfile/PassportType";
import { SaveVISAType, VISAType } from "../Types/CustomerProfile/VISAType";

export const validatePassport = (
  passport: SavePassportRequestType
): boolean => {
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

export const validatePassportFields = (field: string, value: any): boolean => {
  const alphaCheckRegex = /^[a-zA-Z\s]*$/;
  const passportRegex = /^[A-Za-z0-9]*$/;

  if (field === "lastName" && !alphaCheckRegex.test(value)) {
    notifyError("Last name can only contain letters");
    return false;
  }

  if (field === "otherNames" && !alphaCheckRegex.test(value)) {
    notifyError("Other names can only contain letters");
    return false;
  }

  if (field === "placeOfBirth" && !alphaCheckRegex.test(value)) {
    notifyError("Place of birth can only contain letters");
    return false;
  }

  if (field === "passportNumber" && !passportRegex.test(value)) {
    notifyError(`Passport number can only contain letters and numbers`);
    return false;
  }

  return true;
};

export const validateVISA = (visa: SaveVISAType): boolean => {
  let valid = true;

  if (!visa.CountryId) {
    notifyError("Country is required");
    valid = false;
  }

  if (!visa.ExpiryDate) {
    notifyError("VISA expiry date is required");
    valid = false;
  }

  if (!visa.IssuedDate) {
    notifyError("VISA issued date is required");
    valid = false;
  }

  if (!visa.IssuedPlace) {
    notifyError("VISA issued place is required");
    valid = false;
  }

  if (!visa.VisaNumber) {
    notifyError("VISA number is required");
    valid = false;
  }

  if (visa.ExpiryDate! < visa.IssuedDate!) {
    notifyError("VISA expiry date cannot be less than issued date");
    valid = false;
  }

  return valid;
};

export const convertVisaTypeToSaveVisaType = (visa: VISAType): SaveVISAType => {
  return {
    Id: visa.id,
    VisaNumber: visa.visaNumber,
    CountryId: visa.countryId,
    IssuedPlace: visa.issuedPlace,
    IssuedDate: visa.issuedDate,
    ExpiryDate: visa.expireDate,
    CustomerProfileId: "",
    SystemUserId: "9",
    PassportId: "",
  };
};

export const convertPassportTypeToSavePassportRequestType = (
  passport: PassportType
): SavePassportRequestType => {
  return {
    Id: passport.id,
    CustomerProfileId: "",
    SystemUserId: "9",
    ParentId: "",
    LastName: passport.lastName,
    OtherNames: passport.otherNames,
    PassportNumber: passport.passportNumber,
    Gender: passport.genderId,
    DateOfBirth: passport.dateOfBirth?.toLocaleDateString("en-gb") ?? "",
    PlaceOfBirth: passport.placeOfBirth,
    ExpiryDate: passport.passportExpiryDate?.toLocaleDateString("en-gb") ?? "",
    NationalityId: passport.nationalityId,
    CountryId: passport.countryId,
    IsPrimary: passport.isPrimary,
    SalutationId: passport.salutationId,
  };
};
