export type VISAType = {
  id: string;
  visaNumber: string;
  countryId: string;
  countryName: string;
  issuedPlace: string;
  issuedDate: Date | null;
  expireDate: Date | null;
  assignedToPrimaryPassport: boolean;
  passportNumber: string;
};

export type SaveVISAType = {
  Id: string;
  VisaNumber: string;
  CountryId: string;
  IssuedPlace: string;
  IssuedDate: Date | null;
  ExpiryDate: Date | null;
  CustomerProfileId: string | null;
  SystemUserId: string;
  PassportId: string;
};

export type SaveVISAResponseType = {
  VisaId: string;
};
