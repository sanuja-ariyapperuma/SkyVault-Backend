export type PassportType = {
  id: string;
  salutationId: string;
  lastName: string;
  otherNames: string;
  nationalityId: string;
  genderId: string;
  placeOfBirth: string;
  passportNumber: string;
  dateOfBirth: Date | null;
  passportExpiryDate: Date | null;
  countryId: string;
  IsPrimary: string;
};

export type SavePassportRequestType = {
  Id: string | null;
  CustomerProfileId: string | null;
  SystemUserId: string;
  ParentId: string;
  LastName: string;
  OtherNames: string;
  PassportNumber: string;
  Gender: string;
  DateOfBirth: string;
  PlaceOfBirth: string;
  ExpiryDate: string;
  NationalityId: string;
  CountryId: string;
  IsPrimary: string;
  SalutationId: string;
};

export type SavePassportResponseType = {
  customerProfileId: string;
  passportId: string;
};
