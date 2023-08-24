import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeEntryComponent } from './home-entry.component';

describe('HomeEntryComponent', () => {
  let component: HomeEntryComponent;
  let fixture: ComponentFixture<HomeEntryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HomeEntryComponent]
    });
    fixture = TestBed.createComponent(HomeEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
