import { CommunicationMethod } from "./CommunicationMethodEnum";
import { PassportType } from "./PassportType";
import { VISAType } from "./VISAType";

export type CustomerProfileType = {
  PrimaryPassport: PassportType;
  SecondaryPassport: PassportType | null | undefined;
  VISAs: VISAType[] | null | undefined;
  FrequentFlyerNumber: string[];
  PreferredCommunicationMethod: CommunicationMethod;
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
