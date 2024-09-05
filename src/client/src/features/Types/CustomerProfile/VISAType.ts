export type VISAType = {
  id: string;
  visaNumber: string;
  countryId: string;
  countryName: string;
  issuedPlace: string;
  issuedDate: string;
  expireDate: string;
  assignedToPrimaryPassport: boolean;
  passportNumber: string;
};

export type SaveVISAType = {
  Id: string;
  VisaNumber: string;
  CountryId: string;
  IssuedPlace: string;
  IssuedDate: string;
  ExpiryDate: string;
  CustomerProfileId: string | null;
  PassportId: string;
};

export type SaveVISAResponseType = {
  VisaId: string;
};
