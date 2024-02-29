import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import {
  Validators,
  FormControl,
  FormGroup,
  FormBuilder,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, filter, takeUntil } from 'rxjs';
import {
  AdminApiPostApiClient,
  PostDto,
  UpdatePostDto,
  AdminApiBlogApiClient,
  CreatePostDto,
  UserDto,
  PostCategoryDto,
} from 'src/app/api/admin-api.service.generated';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { environment } from 'src/environments/environment';
import { UploadService } from 'src/app/shared/services/upload.service';

@Component({
  templateUrl: 'post-detail.component.html',
})
export class PostDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as PostDto;
  public thumbnailImage;
  filteredPostCategories: PostCategoryDto[];
  filteredUsers: UserDto[];

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private postService: AdminApiPostApiClient,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private blogService: AdminApiBlogApiClient,
    private uploadService: UploadService
  ) {}

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public generateSlug() {
    var slug = this.utilService.makeSeoTitle(this.form.get('name').value);
    this.form.controls['slug'].setValue(slug);
  }

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
      this.saveBtnName = 'Cập nhật';
      this.closeBtnName = 'Hủy';
    } else {
      this.saveBtnName = 'Thêm';
      this.closeBtnName = 'Đóng';
    }
  }

  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    code: [
      { type: 'required', message: 'Bạn phải nhập mã' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    name: [
      { type: 'required', message: 'Bạn phải nhập tên' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    slug: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.postService
      .getPostById(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PostDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  onFileChange(event) {
    if (event.target.files && event.target.files.length) {
      this.uploadService.uploadImage('posts', event.target.files).subscribe({
        next: (response: any) => {
          this.form.controls['thumbnail'].setValue(response.path);
          this.thumbnailImage = environment.API_URL + response.path;
        },
        error: (err: any) => {
          console.log(err);
        },
      });
    }
  }

  saveChange() {
    this.toggleBlockUI(true);
    this.saveData();
  }

  private saveData() {
    console.log(this.form.value);

    if (this.utilService.isEmpty(this.config.data?.id)) {
      var createPostDto = new CreatePostDto({
        ...this.form.value,
        categoryId: this.form.value.slPostCategory?.id,
        authorUserId: this.form.value.slUser?.id,
      });
      this.postService
        .createPost(new CreatePostDto(createPostDto))
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(createPostDto);
          this.toggleBlockUI(false);
        });
    } else {
      var updatePostDto = new UpdatePostDto({
        ...this.form.value,
        id: this.config.data?.id,
        categoryId: this.form.value.slPostCategory?.id,
        authorUserId: this.form.value.slUser?.id,
      });
      this.postService
        .updatePost(updatePostDto)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(updatePostDto);
        });
    }
  }

  buildForm() {
    this.form = this.fb.group({
      code: new FormControl(
        this.selectedEntity.code || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
      slug: new FormControl(
        this.selectedEntity.slug || null,
        Validators.required
      ),
      slPostCategory: new FormControl(
        this.selectedEntity.categoryId
          ? `${this.selectedEntity.categoryCode} - ${this.selectedEntity.categoryName}`
          : null
      ),
      slUser: new FormControl(
        this.selectedEntity.authorUserId
          ? `${this.selectedEntity.authorUserName}`
          : null
      ),
      seoDescription: new FormControl(
        this.selectedEntity.seoDescription || null
      ),
      description: new FormControl(this.selectedEntity.description || null),
      isActive: new FormControl(this.selectedEntity.isActive || false),
      tags: new FormControl(this.selectedEntity.tags || null),
      content: new FormControl(this.selectedEntity.content || null),
      thumbnail: new FormControl(this.selectedEntity.thumbnail || null),
    });
    if (this.form.get('code').value === null) {
      this.generateRandomCode();
    }
    if (this.selectedEntity.thumbnail) {
      this.thumbnailImage = environment.API_URL + this.selectedEntity.thumbnail;
    }
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }

  generateRandomCode() {
    const randomCode = this.utilService.generateRandomCode();
    this.form.controls['code'].setValue(randomCode);
  }

  filterPostCategories(event): void {
    var filter = event.query;
    this.blogService
      .getAllBlogPostCategories(filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((res) => {
        this.filteredPostCategories = res;
      });
  }

  public selectedPostCategoryDisplay(postCategory: PostCategoryDto): string {
    return `${postCategory.code} - ${postCategory.name}`;
  }

  filterUsers(event): void {
    var filter = event.query;
    this.blogService
      .getAllBlogUsers(filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((res) => {
        this.filteredUsers = res;
      });
  }

  public selectedUserDisplay(user: UserDto): string {
    return `${user.userName}`;
  }
}
