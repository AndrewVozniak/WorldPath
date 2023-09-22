package com.worldpath.community.controller;

import com.worldpath.community.DTO.CategoryDTO;
import com.worldpath.community.service.CategoryService;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/categories")
public class CategoryController {
    private final CategoryService categoryService;

    public CategoryController(CategoryService CategoryService) {
        this.categoryService = CategoryService;
    }

    /**
     * @Description Create category
     * @param categoryDTO CategoryDTO
     * @return CategoryDTO
     */
    @PostMapping("/")
    public ResponseEntity<CategoryDTO> createCategory(@RequestBody CategoryDTO categoryDTO) {
        return ResponseEntity.ok(categoryService.createCategory(categoryDTO));
    }

    @GetMapping("/")
    public ResponseEntity<List<CategoryDTO>> getAllCategories() {
        return ResponseEntity.ok(categoryService.getAllCategories());
    }

    @PutMapping("/{id}")
    public ResponseEntity<CategoryDTO> editCategoryById(@RequestBody CategoryDTO categoryDTO, @PathVariable String id) {
        return ResponseEntity.ok(categoryService.editCategoryById(categoryDTO, id));
    }
}
