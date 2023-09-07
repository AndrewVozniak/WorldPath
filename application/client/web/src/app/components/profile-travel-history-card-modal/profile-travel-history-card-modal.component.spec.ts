import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileTravelHistoryCardModalComponent } from './profile-travel-history-card-modal.component';

describe('ProfileTravelHistoryCardModalComponent', () => {
  let component: ProfileTravelHistoryCardModalComponent;
  let fixture: ComponentFixture<ProfileTravelHistoryCardModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileTravelHistoryCardModalComponent]
    });
    fixture = TestBed.createComponent(ProfileTravelHistoryCardModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
