export type VISAType = {
  id: string;
  visaNumber: string;
  countryId: string;
  issuedPlace: string;
  issuedDate: Date | null;
  expiryDate: Date | null;
  assignedToPrimaryPassport: boolean;
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
