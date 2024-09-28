export type PassportType = {
  id: string;
  salutationId: string;
  lastName: string;
  otherNames: string;
  nationalityId: string;
  genderId: string;
  placeOfBirth: string;
  passportNumber: string;
  dateOfBirth: string;
  passportExpiryDate: string;
  countryId: string;
  isPrimary: string;
  parentId?: string;
  passportCode?: string;
};

export type SavePassportRequestType = {
  Id: string | null;
  CustomerProfileId: string | null;
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
  parentId?: string;
};

export type SavePassportResponseType = {
  customerProfileId: string;
  passportId: string;
};
